using MediaSuggesterAPIv2.Api.Models;
using MediaSuggesterAPIv2.Domain.Repositories;
using MediaSuggesterAPIv2.Models;
using Microsoft.AspNetCore.Mvc;

namespace MediaSuggesterAPIv2.Controllers
{
    [ApiController]
    [Route("/")]
    public class SuggestionController : ControllerBase
    {
        private readonly ILogger<SuggestionController> _logger;
        private readonly ISuggestionRepository _repository;

        public SuggestionController(ILogger<SuggestionController> logger, ISuggestionRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpPost(Name = "GetSuggestionsBasedOnReview")]
        public void voidGeneratePersonalizedSuggestion(DtoMediaReview dto)
        {
            //Temporário enquanto implemento lógica da IA
            //return Enumerable.Empty<Suggestion>();
            _repository.GetSuggestions("2k8E8NdnVtOwg7qAinmu2bl8Dch1");
        }
    }
}
