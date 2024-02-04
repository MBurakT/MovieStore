using System;

namespace WebApi.Entities;

public class Purchase
{
    public double Price { get; set; }
    public DateTime PurchaseTime { get; set; }

    public int CustomerId { get; set; }
    public Customer? Customer { get; set; }
    public int MovieId { get; set; }
    public Movie? Movie { get; set; }
}