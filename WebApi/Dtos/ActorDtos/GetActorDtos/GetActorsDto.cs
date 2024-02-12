using System.Collections.Generic;

namespace WebApi.Dtos.ActorDtos.GetActorDtos;

public class GetActorsDto
{
    public List<GetActorDto>? Actors { get; set; }
}