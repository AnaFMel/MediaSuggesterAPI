using Newtonsoft.Json;

namespace MediaSuggesterAPI.Models
{
    public class Candidate
    {
        [JsonProperty("content")]
        public Content Content { get; set; }
    }
}
