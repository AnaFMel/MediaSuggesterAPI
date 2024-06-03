using Newtonsoft.Json;

namespace MediaSuggesterAPI.Models
{
    public class Part
    {
        [JsonProperty("text")]
        public string Text { get; set; }
    }
}
