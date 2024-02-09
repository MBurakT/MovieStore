using System;
using System.Collections.Generic;

namespace WebApi.Dtos.GenreDtos;

public class GetGenreDto
{
    public string Name { get; set; } = string.Empty;

    public List<GetGenreMovieDto>? Movies { get; set; }

    public class GetGenreMovieDto
    {
        public string Name { get; set; } = string.Empty;
        public DateTime ReleaseDate { get; set; }
        public double Price { get; set; }
    }
}