using System.Collections.Generic;

namespace WebApi.Dtos.CustomerDtos.GetCustomerDtos;

public class GetCustomersDto
{
    public List<GetCustomerDto>? Customers { get; set; }
}