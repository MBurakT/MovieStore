using System;
using System.Collections.Generic;

namespace WebApi.Models.MovieModels.GetMoviesQuery;

public class GetMoviesQueryMovieModel
{
    public string? Name { get; set; }
    public DateTime? ReleaseDate { get; set; }
    public double? Price { get; set; }
    public GetMoviesQueryGenreModel? Genre { get; set; }
    public GetMoviesQueryDirectorModel? Director { get; set; }
    public ICollection<GetMoviesQueryActorModel>? Actors { get; set; }
}

public class GetMoviesQueryGenreModel
{
    public string? Name { get; set; }
}

public class GetMoviesQueryDirectorModel
{
    public string? Name { get; set; }
    public string? Surname { get; set; }
}

public class GetMoviesQueryActorModel
{
    public string? Name { get; set; }
    public string? Surname { get; set; }
}