namespace MediaSuggesterAPI.Models
{
    public interface ISuggestions
    {
        public List<Dictionary<string, List<Media>>> Midias { get; set; }
    }
}
