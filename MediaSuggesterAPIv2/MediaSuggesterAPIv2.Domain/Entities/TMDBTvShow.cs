using Newtonsoft.Json;

namespace MediaSuggesterAPIv2.Domain.Entities
{
    public class TMDBTvShow : TMDBGenericMedia
    {
        [JsonProperty("name")]
        public string Titulo { get; set; }
    }
}
