using AutoMapper;
using WebApi.Dtos.GenreDtos;
using WebApi.Dtos.GenreDtos.PostGenreDtos;
using WebApi.Dtos.GenreDtos.PutGenreDtos;
using WebApi.Entities;

namespace WebApi.MappingProfiles;

public class GenreProfile : Profile
{
    public GenreProfile()
    {
        CreateMap<Genre, GetGenreDto>();
        CreateMap<Movie, GetGenreDto.GetGenreMovieDto>();

        CreateMap<AddGenreDto, Genre>();

        CreateMap<UpdateGenreDto, Genre>();
    }
}