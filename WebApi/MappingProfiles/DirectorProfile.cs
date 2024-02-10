using AutoMapper;
using WebApi.Dtos.DirectorDtos.GetDirectorDtos;
using WebApi.Entities;

namespace WebApi.MappingProfiles;

public class DirectorProfile : Profile
{
    public DirectorProfile()
    {
        CreateMap<Director, GetDirectorDto>();
        CreateMap<Movie, GetDirectorDto.GetDirectorMovieDto>();
    }
}