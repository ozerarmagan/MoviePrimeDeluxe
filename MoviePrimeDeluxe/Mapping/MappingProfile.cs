using AutoMapper;
using MoviePrimeDeluxe.Entities;
using MoviePrimeDeluxe.Entities.DTO;

namespace MoviePrimeDeluxe.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<MovieDTO, Movie>().ReverseMap();
        }
    }
}
