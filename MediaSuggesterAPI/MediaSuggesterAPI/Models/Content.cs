using Newtonsoft.Json;

namespace MediaSuggesterAPI.Models
{
    public class Content
    {
        [JsonProperty("parts")]
        public List<Part> Parts { get; set; }
    }
}