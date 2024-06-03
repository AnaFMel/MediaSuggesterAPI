namespace MediaSuggesterAPI.Models
{
    public interface Suggestions
    {
        public List<Dictionary<string, List<Media>>> Midias { get; set; }
    }
}
