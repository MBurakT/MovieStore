using System.Collections.Generic;

namespace WebApi.Entities;

public class Director
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }

    public ICollection<Movie>? Movies { get; set; }

    public Director(string name, string surname)
    {
        Name = name;
        Surname = surname;
    }
}