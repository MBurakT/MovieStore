using AutoMapper;
using WebApi.Entities;
using WebApi.Models.MovieModels.GetMovieCommandById;
using WebApi.Models.MovieModels.GetMoviesQuery;

namespace WebApi.MappingProfiles;

public class MovieProfile : Profile
{
    public MovieProfile()
    {
        CreateMap<Movie, GetMoviesQueryMovieModel>();
        CreateMap<Genre, GetMoviesQueryGenreModel>();
        CreateMap<Director, GetMoviesQueryDirectorModel>();
        CreateMap<Actor, GetMoviesQueryActorModel>();

        CreateMap<Movie, GetMovieCommandMovieModel>();
        CreateMap<Genre, GetMovieCommandGenreModel>();
        CreateMap<Director, GetMovieCommandDiretorModel>();
        CreateMap<Actor, GetMovieCommandActorModel>();
    }
}