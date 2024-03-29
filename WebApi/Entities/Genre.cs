using System.Collections.Generic;

namespace WebApi.Entities;

public class Genre
{
    public int Id { get; set; }
    public string Name { get; set; }

    public ICollection<Movie>? Movies { get; set; }

    public Genre(string name)
    {
        Name = name;
    }
}