using Newtonsoft.Json;

namespace MediaSuggesterAPIv2.Domain.Entities
{
    public class TMDBMovie : TMDBGenericMedia
    {
        [JsonProperty("title")]
        public string Titulo { get; set; }
    }
}
