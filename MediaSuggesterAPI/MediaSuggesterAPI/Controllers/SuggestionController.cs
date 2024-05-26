using MediaSuggesterAPI.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace MediaSuggesterAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SuggestionController : Controller
    {
        private readonly SuggestionService _suggestionService;

        public SuggestionController(SuggestionService suggestionService)
        {
            _suggestionService = suggestionService;
        }

        [HttpPost(Name = "Suggestions")]
        public JsonResult Suggestions(DtoGetSuggestion dto)
        {
            var suggestions = _suggestionService.ObterSugestoes(dto);

            return Json(suggestions);
        }
    }
}
