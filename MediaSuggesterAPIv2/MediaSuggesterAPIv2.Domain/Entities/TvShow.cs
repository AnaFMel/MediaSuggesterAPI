using Newtonsoft.Json;

namespace MediaSuggesterAPIv2.Domain.Entities
{
    public class TvShow : GenericMedia
    {
        [JsonProperty("name")]
        public string Titulo { get; set; }
    }
}
