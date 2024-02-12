using AutoMapper;
using WebApi.Dtos.CustomerDtos.GetCustomerDtos;
using WebApi.Dtos.CustomerDtos.PostCustomerDtos;
using WebApi.Dtos.CustomerDtos.PutCustomerDtos;
using WebApi.Entities;

namespace WebApi.MappingProfiles;

public class CustomerProfile : Profile
{
    public CustomerProfile()
    {
        CreateMap<Customer, GetCustomerDto>();
        CreateMap<Genre, GetCustomerDto.GetCustomerGenreDto>();
        CreateMap<Purchase, GetCustomerDto.GetCustomerPurchaseDto>();
        CreateMap<Movie, GetCustomerDto.GetCustomerMovieDto>();

        CreateMap<AddCustomerDto, Customer>();

        CreateMap<UpdateCustomerDto, Customer>();
    }
}