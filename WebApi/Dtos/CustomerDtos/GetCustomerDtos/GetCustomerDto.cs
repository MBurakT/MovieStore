using System;
using System.Collections.Generic;

namespace WebApi.Dtos.CustomerDtos.GetCustomerDtos;

public class GetCustomerDto
{
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;

    public List<GetCustomerGenreDto>? FavoriteGenres { get; set; }
    public List<GetCustomerPurchaseDto>? Purchases { get; set; }
    public List<GetCustomerMovieDto>? Movies { get; set; }

    public class GetCustomerGenreDto
    {
        public string Name { get; set; } = string.Empty;
    }

    public class GetCustomerPurchaseDto
    {
        public double Price { get; set; }
        public DateTime PurchaseTime { get; set; }
        public string MovieName { get; set; } = string.Empty;
    }

    public class GetCustomerMovieDto
    {
        public string Name { get; set; } = string.Empty;
    }
}