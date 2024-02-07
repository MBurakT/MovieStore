using System;

namespace WebApi.Models.MovieDtos;

public class MovieDto
{
    public string? Name { get; set; }
    public DateTime? ReleaseDate { get; set; }
    public double? Price { get; set; }
    public string? Genre { get; set; }
    public string? Director { get; set; }
    public string[]? Actors { get; set; }
}