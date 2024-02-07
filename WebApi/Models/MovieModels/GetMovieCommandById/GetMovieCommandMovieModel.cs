using System;
using System.Collections.Generic;

namespace WebApi.Models.MovieModels.GetMovieCommandById;

public class GetMovieCommandMovieModel
{
    public string? Name { get; set; }
    public DateTime? ReleaseDate { get; set; }
    public double? Price { get; set; }
    public GetMovieCommandGenreModel? Genre { get; set; }
    public GetMovieCommandDiretorModel? Director { get; set; }
    public ICollection<GetMovieCommandActorModel>? Actors { get; set; }
}

public class GetMovieCommandGenreModel
{
    public string? Name { get; set; }
}

public class GetMovieCommandDiretorModel
{
    public string? Name { get; set; }
    public string? Surname { get; set; }
}

public class GetMovieCommandActorModel
{
    public string? Name { get; set; }
    public string? Surname { get; set; }
}