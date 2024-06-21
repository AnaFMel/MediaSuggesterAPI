using Newtonsoft.Json;

namespace MediaSuggesterAPI.Models
{
    public class SuggestionsSeries : ISuggestions
    {
        [JsonProperty("series")]
        public List<Dictionary<string, List<Media>>> Midias { get; set; }
    }
}