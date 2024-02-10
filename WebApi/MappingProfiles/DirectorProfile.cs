using AutoMapper;
using WebApi.Dtos.DirectorDtos.GetDirectorDtos;
using WebApi.Dtos.DirectorDtos.PostDirectorDtos;
using WebApi.Dtos.DirectorDtos.PutDirectorDtos;
using WebApi.Entities;

namespace WebApi.MappingProfiles;

public class DirectorProfile : Profile
{
    public DirectorProfile()
    {
        CreateMap<Director, GetDirectorDto>();
        CreateMap<Movie, GetDirectorDto.GetDirectorMovieDto>();

        CreateMap<AddDirectorDto, Director>();

        CreateMap<UpdateDirectorDto, Director>();
    }
}