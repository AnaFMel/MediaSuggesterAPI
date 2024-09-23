using Newtonsoft.Json;

namespace MediaSuggesterAPIv2.Domain.Entities
{
    public class Movie : GenericMedia
    {
        [JsonProperty("title")]
        public string Titulo { get; set; }
    }
}
