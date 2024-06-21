using Newtonsoft.Json;

namespace MediaSuggesterAPI.Models
{
    public class Response
    {
        [JsonProperty("candidates")]
        public List<Candidate> Candidates { get; set; }
    }
}