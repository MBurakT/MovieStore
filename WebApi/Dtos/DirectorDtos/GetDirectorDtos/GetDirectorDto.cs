using System;
using System.Collections.Generic;

namespace WebApi.Dtos.DirectorDtos.GetDirectorDtos;

public class GetDirectorDto
{
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public List<GetDirectorMovieDto>? Movies { get; set; }

    public class GetDirectorMovieDto
    {
        public string Name { get; set; } = string.Empty;
        public DateTime ReleaseDate { get; set; }
    }
}