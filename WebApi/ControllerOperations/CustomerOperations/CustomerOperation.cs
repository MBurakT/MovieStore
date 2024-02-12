using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;
using WebApi.Dtos.CustomerDtos.GetCustomerDtos;
using WebApi.Dtos.CustomerDtos.PostCustomerDtos;
using WebApi.Dtos.CustomerDtos.PutCustomerDtos;
using WebApi.Entities;

namespace WebApi.ControllerOperations.CustomerOperations;

public class CustomerOperation
{
    readonly IMovieStoreDbContext _context;
    readonly IMapper _mapper;

    public CustomerOperation(IMovieStoreDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public GetCustomersDto GetCustomersQuery()
    {
        IQueryable<Customer> dbCustomer = _context.Customers.AsNoTracking();
        IQueryable<CustomerGenre> dbCustomerGenre = _context.CustomerGenres.AsNoTracking();
        IQueryable<Purchase> dbPurchase = _context.Purchases.AsNoTracking();
        IQueryable<Genre> dbGenre = _context.Genres.AsNoTracking();
        IQueryable<Movie> dbMovie = _context.Movies.AsNoTracking().Where(x => !x.IsDeleted);

        if (!dbCustomer.Any()) return new GetCustomersDto { Customers = new() };

        List<Customer> customers = dbCustomer.ToList();

        customers.ForEach(x =>
        {
            IQueryable<int> genreIds = dbCustomerGenre.Where(y => y.CustomerId == x.Id).Select(y => y.GenreId);
            IQueryable<int> movieIds = dbPurchase.Where(y => y.CustomerId == x.Id).Select(y => y.MovieId);

            x.FavoriteGenres = dbGenre.Where(y => genreIds.Contains(y.Id)).ToList();
            x.Movies = dbMovie.Where(y => movieIds.Contains(y.Id)).ToList();
            x.Purchases = dbPurchase.Where(y => y.CustomerId == x.Id).Include(y => y.Movie).AsNoTracking().ToList();
        });

        return new GetCustomersDto { Customers = _mapper.Map<List<GetCustomerDto>>(customers) };
    }

    public GetCustomerDto GetCustomerCommandById(int id)
    {
        if (id < 1) throw new InvalidOperationException("Customer does not exist!");

        IQueryable<Customer> dbCustomer = _context.Customers.AsNoTracking();

        if (!dbCustomer.Any(x => x.Id == id)) throw new InvalidOperationException("Customer does not exist!");

        Customer customer = dbCustomer.Single(x => x.Id == id);

        customer.FavoriteGenres = _context.Genres.AsNoTracking().Where(
            x => _context.CustomerGenres.AsNoTracking().Where(y => y.CustomerId == id).Select(y => y.GenreId).Contains(x.Id)).ToList();

        customer.Movies = _context.Movies.AsNoTracking().Where(
            x => !x.IsDeleted && _context.Purchases.AsNoTracking().Where(y => y.CustomerId == id).Select(y => y.MovieId).Contains(x.Id)).ToList();

        customer.Purchases = _context.Purchases.AsNoTracking().Where(x => x.CustomerId == id).Include(y => y.Movie).AsNoTracking().ToList();

        return _mapper.Map<GetCustomerDto>(customer);
    }

    public void AddCustomerCommand(AddCustomerDto addCustomerDto)
    {
        IQueryable<Customer> dbCustomer = _context.Customers.AsNoTracking();

        if (!dbCustomer.Any(x => x.Name.Equals(addCustomerDto.Name) && x.Surname.Equals(addCustomerDto.Surname)))
            throw new InvalidOperationException("Customer does not exist!");

        _context.Customers.Add(_mapper.Map<Customer>(addCustomerDto));
        _context.SaveChanges();
    }

    public void UpdateCustomerCommand(int id, UpdateCustomerDto updateCustomerDto)
    {
        if (id < 1) throw new InvalidOperationException("Customer does not exist!");

        IQueryable<Customer> dbCustomer = _context.Customers.AsNoTracking();

        if (!dbCustomer.Any(x => x.Id == id)) throw new InvalidOperationException("Customer does not exist!");

        Customer customer = _mapper.Map<Customer>(updateCustomerDto);
        customer.Id = id;

        _context.Customers.Update(customer);
        _context.SaveChanges();
    }
}