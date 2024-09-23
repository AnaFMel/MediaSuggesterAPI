using AutoMapper;
using MediaSuggesterAPIv2.Api.Models;
using MediaSuggesterAPIv2.Domain.Entities;

namespace MediaSuggesterAPIv2.Api.Profiles
{
    public class DomainProfile : Profile
    {
        public DomainProfile()
        {
            CreateMap<Review, DtoMediaReview>().ReverseMap();

        }
    }
}
