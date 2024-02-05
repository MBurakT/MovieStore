using System.Collections.Generic;

namespace WebApi.Entities;

public class Customer
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }

    public ICollection<CustomerGenre>? CustomerGenres { get; set; }
    public ICollection<Purchase>? Purchases { get; set; }

    public Customer(string name, string surname)
    {
        Name = name;
        Surname = surname;
    }
}