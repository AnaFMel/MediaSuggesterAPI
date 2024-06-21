using Newtonsoft.Json;

namespace MediaSuggesterAPI.Models
{
    public interface Suggestions
    {
        public List<Dictionary<string, List<Media>>> Midias { get; set; }
    }

    public class SuggestionsSeries : Suggestions
    {
        [JsonProperty("series")]
        public List<Dictionary<string, List<Media>>> Midias { get; set; }
    }

    public class SuggestionsFilmes : Suggestions
    {
        [JsonProperty("filmes")]
        public List<Dictionary<string, List<Media>>> Midias { get; set; }
    }

    public class Media
    {
        public string Titulo { get; set; }
        public string OndeAssistir { get; set; }
    }

    public class Response
    {
        [JsonProperty("candidates")]
        public List<Candidate> Candidates { get; set; }
    }

    public class Candidate
    {
        [JsonProperty("content")]
        public Content Content { get; set; }
    }

    public class Content
    {
        [JsonProperty("parts")]
        public List<Part> Parts { get; set; }
    }

    public class Part
    {
        [JsonProperty("text")]
        public string Text { get; set; }
    }
}
