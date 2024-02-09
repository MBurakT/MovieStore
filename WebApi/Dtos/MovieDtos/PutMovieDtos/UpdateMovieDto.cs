using System;
using System.Collections.Generic;

namespace WebApi.Dtos.MovieDtos.PutMovieDtos;

public class UpdateMovieDto
{
    public string? Name { get; set; }
    public DateTime? ReleaseDate { get; set; }
    public double? Price { get; set; }
    public UpdateMovieGenreDto? Genre { get; set; }
    public UpdateMovieDirectorDto? Director { get; set; }
    public List<UpdateMovieActorDto>? Actors { get; set; }

    public class UpdateMovieGenreDto
    {
        public string? Name { get; set; }
    }

    public class UpdateMovieDirectorDto
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
    }

    public class UpdateMovieActorDto
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
    }
}