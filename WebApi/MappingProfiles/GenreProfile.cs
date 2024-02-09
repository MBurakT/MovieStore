using AutoMapper;
using WebApi.Dtos.GenreDtos;
using WebApi.Entities;

namespace WebApi.MappingProfiles;

public class GenreProfile : Profile
{
    public GenreProfile()
    {
        CreateMap<Genre, GetGenreDto>();
        CreateMap<Movie, GetGenreDto.GetGenreMovieDto>();
    }
}