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

    public Purchase(double price, DateTime purchaseTime, int customerId, int movieId)
    {
        Price = price;
        PurchaseTime = purchaseTime;
        CustomerId = customerId;
        MovieId = movieId;
    }

    public Purchase(double price, DateTime purchaseTime, Customer customer, Movie movie)
    {
        Price = price;
        PurchaseTime = purchaseTime;
        Customer = customer;
        Movie = movie;
    }
}