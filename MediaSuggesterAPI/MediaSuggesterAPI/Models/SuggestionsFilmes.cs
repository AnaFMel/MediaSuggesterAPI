using Newtonsoft.Json;

namespace MediaSuggesterAPI.Models
{
    public class SuggestionsFilmes : ISuggestions
    {
        [JsonProperty("filmes")]
        public List<Dictionary<string, List<Media>>> Midias { get; set; }
    }
}