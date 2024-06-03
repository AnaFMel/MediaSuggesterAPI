using Newtonsoft.Json;

namespace MediaSuggesterAPI.Models
{
    public class SuggestionsSeries : Suggestions
    {
        [JsonProperty("series")]
        public List<Dictionary<string, List<Media>>> Midias { get; set; }
    }
}
