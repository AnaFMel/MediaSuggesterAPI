using Newtonsoft.Json;

namespace MediaSuggesterAPI.Models
{
    public class Sinopse
    {
        [JsonProperty("sinopse")]
        public string Texto { get; set; }
    }
}
