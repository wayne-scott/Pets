using Newtonsoft.Json;

namespace Pets.DomainModel
{
    /// <summary>
    /// Encapsulates all pet business logic.
    /// </summary>
    public class Pet
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonIgnore]
        public bool IsCat => Type == "Cat";
    }
}
