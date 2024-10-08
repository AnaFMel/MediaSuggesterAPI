using AutoMapper;
using MediaSuggesterAPIv2.Api.Models;
using MediaSuggesterAPIv2.Domain.Entities;
using MediaSuggesterAPIv2.Domain.Repositories;
using MediaSuggesterAPIv2.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace MediaSuggesterAPIv2.Controllers
{
    [ApiController]
    [Route("/")]
    public class SuggestionController : ControllerBase
    {
        private readonly ISuggestionRepository _repository;
        private readonly SuggestionService _service;
        private readonly IMapper _mapper;

        public SuggestionController(ISuggestionRepository repository, SuggestionService service, IMapper mapper)
        {
            _repository = repository;
            _service = service;
            _mapper = mapper;
        }

        [HttpPost(Name = "GetSuggestionsBasedOnReview")]
        public void voidGeneratePersonalizedSuggestion(DtoMediaReview dto)
        {
            var review = _mapper.Map<Review>(dto);

            _service.GetSuggestionsBasedOnReviews(review);

            //return Ok();
        }
    }
}
