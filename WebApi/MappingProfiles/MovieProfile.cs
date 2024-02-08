using AutoMapper;
using WebApi.Dtos.MovieDtos.PostMovieDtos;
using WebApi.Entities;
using WebApi.Models.MovieDtos.GetMovieDtos;

namespace WebApi.MappingProfiles;

public class MovieProfile : Profile
{
    public MovieProfile()
    {
        CreateMap<Movie, GetMovieDto>();
        CreateMap<Genre, GetMovieDto.GetMovieGenreDto>();
        CreateMap<Director, GetMovieDto.GetMovieDirectorDto>();
        CreateMap<Actor, GetMovieDto.GetMovieActorDto>();

        CreateMap<AddMovieDto, Movie>();
        CreateMap<AddMovieDto.AddMovieGenreDto, Genre>();
        CreateMap<AddMovieDto.AddMovieDirectorDto, Director>();
        CreateMap<AddMovieDto.AddMovieActorDto, Actor>();
    }
}