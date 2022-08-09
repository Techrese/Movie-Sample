using AutoMapper;
using GenreApi.Dtos;

namespace GenreApi.Models
{
    public class MapperProfiles : Profile
    {
        public MapperProfiles()
        {
            CreateMap<Genre, GenreDto>().ReverseMap();
        }
    }
}
