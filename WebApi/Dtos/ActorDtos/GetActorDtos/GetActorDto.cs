using System;
using System.Collections.Generic;

namespace WebApi.Dtos.ActorDtos.GetActorDtos;

public class GetActorDto
{
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;

    public List<GetActorMovieDto>? Movies { get; set; }

    public class GetActorMovieDto
    {
        public string Name { get; set; } = string.Empty;
        public DateTime ReleaseDate { get; set; }
    }
}