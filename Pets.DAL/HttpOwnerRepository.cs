using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System;
using Pets.DomainModel;
using Newtonsoft.Json.Schema;
using System.IO;
using Newtonsoft.Json.Schema.Generation;
using Microsoft.Extensions.Caching.Memory;

namespace Pets.DAL
{
    public class HttpOwnerRepository : IOwnerRepository
    {
        private readonly IConfiguration _configuration;
        private readonly IMemoryCache _memoryCache;
        /// <summary>
        /// A static HttpClient to prevent excessive socket use.
        /// </summary>
        private static readonly HttpClient HttpClient = new HttpClient();

        public HttpOwnerRepository(IConfiguration configuration, IMemoryCache memoryCache)
        {
            _configuration = configuration;
            _memoryCache = memoryCache;
        }

        private string EndpointURL
        {
            get
            {
                string endpointURL = _configuration["EndpointURL"];
                if (string.IsNullOrEmpty(endpointURL))
                {
                    throw new Exception("Missing configuration: EndpointURL");
                }
                return endpointURL;
            }
        }

        /// <summary>
        /// Provide access to all owners.
        /// </summary>
        /// <returns>A <see cref="Owners"/> <see langword="object"/>.</returns>
        public Owners GetAllOwners()
        {
            var owners = new Owners();
            if (!_memoryCache.TryGetValue(EndpointURL, out List<Owner> listOfOwners))
            {
                var task = HttpClient.GetAsync(EndpointURL)
                  .ContinueWith(taskwithresponse =>
                  {
                      var response = taskwithresponse.Result;
                      if (response.StatusCode == HttpStatusCode.OK)
                      {
                          var jsonString = response.Content.ReadAsStringAsync();
                          jsonString.Wait();
                          listOfOwners = ValidateAndParseResponse(jsonString.Result);
                      }
                  });
                task.Wait();
                _memoryCache.Set(EndpointURL, listOfOwners, TimeSpan.FromMinutes(60));
            }
            owners.Initialise(listOfOwners);
            return owners;
        }

        /// <summary>
        /// Validate the received JSON string against the our object structure.  If it's valid
        /// then create and populate the <see cref="Owner"/> and <see cref="Pet"/> objects.
        /// </summary>
        /// <param name="jsonString">Received JSON string.</param>
        /// <exception cref="System.Exception">Thrown if the response is invalid JSON or mandatory data is missing.</exception>
        /// <returns>List of <see cref="Owner"/> objects.</returns>
        private List<Owner> ValidateAndParseResponse(string jsonString)
        {
            var generator = new JSchemaGenerator();
            var parsedSchema = generator.Generate(typeof(List<Owner>));
            var reader = new JsonTextReader(new StringReader(jsonString));

            var validatingReader = new JSchemaValidatingReader(reader)
            {
                Schema = parsedSchema
            };

            var messages = new List<string>();
            validatingReader.ValidationEventHandler += (o, a) => messages.Add(a.Message);

            try
            {
                JsonSerializer serializer = new JsonSerializer();
                var owners = serializer.Deserialize<List<Owner>>(validatingReader);
                
                if (messages.Count > 0)
                {
                    var exception = new Exception("Invalid Response : Missing Mandatory Data");
                    exception.Data.Add("ValidationErrors", messages);
                    throw exception;
                }

                return owners;
            }
            catch (JsonReaderException ex)
            {
                throw new Exception("Invalid Response : Invalid Json", ex);
            }
        }
    }
}
