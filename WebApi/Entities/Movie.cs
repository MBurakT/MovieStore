using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities;

public class Movie
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime ReleaseDate { get; set; }
    public double Price { get; set; }


    public int GenreId { get; set; }
    public Genre? Genre { get; set; }
    public ICollection<Actor>? Actors { get; set; }
    public int DirectorId { get; set; }
    public Director? Director { get; set; }

    public Movie(string name, DateTime releaseDate, double price, int genreId, int directorId)
    {
        Name = name;
        ReleaseDate = releaseDate;
        Price = price;
        GenreId = genreId;
        DirectorId = directorId;
    }
}