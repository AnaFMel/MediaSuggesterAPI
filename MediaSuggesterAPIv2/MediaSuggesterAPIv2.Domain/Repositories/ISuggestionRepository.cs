using MediaSuggesterAPIv2.Domain.Entities;

namespace MediaSuggesterAPIv2.Domain.Repositories
{
    public interface ISuggestionRepository
    {
        Task<SuggestionList> GetSuggestions(string id);
        void AddSuggestions(SuggestionList suggestionList);
        void UpdateSuggestions(SuggestionList suggestionList);
    }
}
