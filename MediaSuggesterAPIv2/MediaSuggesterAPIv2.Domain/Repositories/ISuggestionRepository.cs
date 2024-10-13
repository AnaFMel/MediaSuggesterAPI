using MediaSuggesterAPIv2.Domain.Entities;
using MediaSuggesterAPIv2.Domain.Tags;
using System.Globalization;

namespace MediaSuggesterAPIv2.Domain.Repositories
{
    public interface ISuggestionRepository
    {
        GenericSuggestionList GetSuggestions(string id);
        void AddPersonalizedSuggestions(string userId, string mediaType, int likedMediaId, int[] suggestionList);
        void UpdateSuggestions(string userId, GenericSuggestionList suggestionList);
    }
}
