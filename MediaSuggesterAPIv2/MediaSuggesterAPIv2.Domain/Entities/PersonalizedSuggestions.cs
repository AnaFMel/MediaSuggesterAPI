using MediaSuggesterAPIv2.Domain.Tags;

namespace MediaSuggesterAPIv2.Domain.Entities
{
    public class PersonalizedSuggestions
    {
        public int LikedMediaId { get; set; }
        public IEnumerable<int> SuggestionsId { get; set; }
        public MediaType TypeofLikedMedia { get; set; }
    }
}
