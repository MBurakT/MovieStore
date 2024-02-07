using System.Linq;
using AutoMapper;
using WebApi.Entities;
using WebApi.Models.MovieModels.GetMovieCommandById;
using WebApi.Models.MovieModels.GetMoviesQuery;

namespace WebApi.MappingProfiles;

public class MovieProfile : Profile
{
    public MovieProfile()
    {
        CreateMap<Movie, GetMoviesQueryMovieModel>()
            .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name))
            .ForMember(dest => dest.Director, opt => opt.MapFrom(src => $"{src.Director.Name} {src.Director.Surname}"))
            .ForMember(dest => dest.Actors, opt => opt.MapFrom(src => src.Actors.Select(x => $"{x.Name} {x.Surname}")))
            .ReverseMap();

        CreateMap<Movie, GetMovieCommandMovieModel>()
            .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name))
            .ForMember(dest => dest.Director, opt => opt.MapFrom(src => $"{src.Director.Name} {src.Director.Surname}"))
            .ForMember(dest => dest.Actors, opt => opt.MapFrom(src => src.Actors.Select(x => $"{x.Name} {x.Surname}")))
            .ReverseMap();
    }
}