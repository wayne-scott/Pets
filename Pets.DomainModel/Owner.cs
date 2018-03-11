using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Pets.DomainModel
{
    /// <summary>
    /// Encapsulates all owner business logic.
    /// </summary>
    public class Owner
    {
        public Owner()
        {
            Pets = new List<Pet>();
        }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("gender")]
        public string Gender { get; set; }

        [JsonProperty("age")]
        public int Age { get; set; }

        [JsonProperty("pets")]
        public List<Pet> Pets { get; set; }

        [JsonIgnore]
        public bool HasCats => Cats.Count() > 0;

        [JsonIgnore]
        public IEnumerable<Pet> Cats
        {
            get { return Pets?.Where(pet => pet.IsCat).ToList() ?? new List<Pet>(); }
        }
    }
}
