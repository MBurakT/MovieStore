using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities;

public class Customer
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }

    public ICollection<Genre>? FavoriteGenres { get; set; }
    public ICollection<Purchase>? Purchases { get; set; }

    public Customer(string name, string surname)
    {
        Name = name;
        Surname = surname;
    }
}