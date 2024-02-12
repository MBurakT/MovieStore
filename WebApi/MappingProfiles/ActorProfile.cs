using AutoMapper;
using WebApi.Dtos.ActorDtos.GetActorDtos;
using WebApi.Dtos.ActorDtos.PostActorDtos;
using WebApi.Dtos.ActorDtos.PutActorDtos;
using WebApi.Entities;

namespace WebApi.MappingProfiles;

public class ActorProfile : Profile
{
    public ActorProfile()
    {
        CreateMap<Actor, GetActorDto>();
        CreateMap<Movie, GetActorDto.GetActorMovieDto>();

        CreateMap<AddActorDto, Actor>();

        CreateMap<UpdateActorDto, Actor>();
    }
}