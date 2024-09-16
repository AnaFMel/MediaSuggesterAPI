using MediaSuggesterAPIv2.Models;
using Microsoft.AspNetCore.Mvc;

namespace MediaSuggesterAPIv2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SuggestionController : ControllerBase
    {
        private readonly ILogger<SuggestionController> _logger;

        public SuggestionController(ILogger<SuggestionController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetSuggestionsBasedOnCreatedProfile")]
        public IEnumerable<Suggestion> Get()
        {
            //Temporário enquanto implemento lógica da IA
            return Enumerable.Empty<Suggestion>();
        }

        [HttpGet(Name = "GetSuggestionsBasedOnOneMedia")]
        public IEnumerable<Suggestion> Get(int mediaId)
        {
            //Temporário enquanto implemento lógica da IA
            return Enumerable.Empty<Suggestion>();
        }
    }
}
