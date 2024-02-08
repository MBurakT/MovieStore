using System;
using System.Collections.Generic;

namespace WebApi.Dtos.MovieDtos.PostMovieDtos;

public class AddMovieDto
{
    public string? Name { get; set; }
    public DateTime? ReleaseDate { get; set; }
    public double? Price { get; set; }
    public AddMovieGenreDto? Genre { get; set; }
    public AddMovieDirectorDto? Director { get; set; }
    public List<AddMovieActorDto>? Actors { get; set; }

    public class AddMovieGenreDto
    {
        public string? Name { get; set; }
    }

    public class AddMovieDirectorDto
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
    }

    public class AddMovieActorDto
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
    }
}