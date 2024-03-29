using System;
using System.Collections.Generic;

namespace WebApi.Entities;

public class Movie
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime ReleaseDate { get; set; }
    public double Price { get; set; }
    public bool IsDeleted { get; set; } = false;


    public int GenreId { get; set; }
    public Genre? Genre { get; set; }
    public int DirectorId { get; set; }
    public Director? Director { get; set; }
    public ICollection<Actor>? Actors { get; set; }

    public Movie(string name, DateTime releaseDate, double price, int genreId, int directorId)
    {
        Name = name;
        ReleaseDate = releaseDate;
        Price = price;
        GenreId = genreId;
        DirectorId = directorId;
    }

    public Movie(string name, DateTime releaseDate, double price, Genre genre, Director director)
    {
        Name = name;
        ReleaseDate = releaseDate;
        Price = price;
        Genre = genre;
        Director = director;
    }

    public Movie(string name, DateTime releaseDate, double price, Genre genre, Director director, ICollection<Actor> actors)
    {
        Name = name;
        ReleaseDate = releaseDate;
        Price = price;
        Genre = genre;
        Director = director;
        Actors = actors;
    }
}