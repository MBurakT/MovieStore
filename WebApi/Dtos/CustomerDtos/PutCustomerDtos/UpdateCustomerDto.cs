using System.Collections.Generic;

namespace WebApi.Dtos.CustomerDtos.PutCustomerDtos;

public class UpdateCustomerDto
{
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public string[]? FavoriteGenres { get; set; }
}