using System;
using System.Collections.Generic;

namespace WebApi.Models.MovieDtos.GetMovieDtos;

public class GetMovieDto
{
    public string? Name { get; set; }
    public DateTime? ReleaseDate { get; set; }
    public double? Price { get; set; }
    public GetMovieGenreDto? Genre { get; set; }
    public GetMovieDirectorDto? Director { get; set; }
    public List<GetMovieActorDto>? Actors { get; set; }

    public class GetMovieGenreDto
    {
        public string? Name { get; set; }
    }

    public class GetMovieDirectorDto
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
    }

    public class GetMovieActorDto
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
    }
}