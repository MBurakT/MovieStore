using System.Collections.Generic;

namespace WebApi.Entities;

public class Customer
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }

    public ICollection<Genre>? FavoriteGenres { get; set; }
    public ICollection<Purchase>? Purchases { get; set; }
}