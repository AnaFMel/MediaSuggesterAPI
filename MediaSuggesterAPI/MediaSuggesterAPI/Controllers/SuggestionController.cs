using MediaSuggesterAPI.Dtos;
using MediaSuggesterAPI.Models;
using MediaSuggesterAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace MediaSuggesterAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SuggestionController : ControllerBase
    {
        private readonly SuggestionService _suggestionService;

        public SuggestionController(SuggestionService suggestionService)
        {
            _suggestionService = suggestionService;
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Obtém sugestões de mídia")]
        [SwaggerResponse(200, "Retorna as sugestões de mídia", typeof(List<DtoGetSuggestion>))]
        public ActionResult<IEnumerable<Suggestions>> Suggestions(DtoGetSuggestion dto)
        {
            var suggestions = _suggestionService.ObterSugestoes(dto);

            //if (suggestions == null) return NotFound();

            return Ok(suggestions);
        }

        [HttpGet("{nomeMidia}"]
        [SwaggerOperation(Summary = "Obtém uma sinopse breve da mídia")]
        [SwaggerResponse(200, "Retorna a sinopse da mídia", typeof(string))]
        public ActionResult<string> Sinopse(string nomeMidia)
        {
            var sinopse = _suggestionService.ObterSinopse(nomeMidia);

            //if (sinopse == string.Empty) return NotFound();

            return Ok(sinopse);
        }
    }
}
