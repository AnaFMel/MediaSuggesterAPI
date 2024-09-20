using MediaSuggesterAPIv2.Domain.Entities;
using MediaSuggesterAPIv2.Domain.Repositories;

namespace MediaSuggesterAPIv2.Domain.Services
{
    public class SuggestionService
    {
        private readonly ISuggestionRepository _suggestionRepository;

        public SuggestionService(ISuggestionRepository suggestionRepository)
        {
            _suggestionRepository = suggestionRepository;
        }

        public void GetSuggestionsBasedOnReviews(Review review)
        {
           var sugestoes_atuais = _suggestionRepository.GetSuggestions(review.UserId);

            //teria que juntar as futuras sugestões personalizadas, talvez

           //depois, entra o consumo da IA, mandando o ReviewText pra analizar e adotando o comportamento necessário para cada resultado.
        }
    }
}
