using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebApi.Entities;

namespace WebApi.DBOperations.DataSeeders;

public class DataSeeder
{
    public static void Seed(IServiceProvider serviceProvider)
    {
        using (MovieStoreDbContext context = new MovieStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<MovieStoreDbContext>>()))
        {
            // context.Database.EnsureCreated();

            List<Genre> genres =
            [
                new Genre("Drama"),
                new Genre("Crime"),
                new Genre("Action"),
                new Genre("History"),
                new Genre("Sci-Fi")
            ];

            List<Director> directors =
            [
                new Director("Robert", "Zemeckis"),
                new Director("Francis Ford", "Coppola"),
                new Director("Christopher", "Nolan"),
                new Director("Steven", "Spielberg")
            ];

            List<Movie> movies =
            [
                new Movie("Forrest Gump", new DateTime(1994, 11, 11), 110, genres[0] , directors[0]),
                new Movie("The Godfather", new DateTime(1973, 10, 1), 120, genres[1] , directors[1]),
                new Movie("The Dark Knight", new DateTime(2008, 6, 25), 130, genres[2] , directors[2]),
                new Movie("Schindler's List", new DateTime(1993, 12, 15), 140, genres[3] , directors[3]),
                new Movie("Inception", new DateTime(2010, 6, 30), 150, genres[4] , directors[2])
            ];

            List<Actor> actors =
            [
                new Actor("Tom", "Hanks"),
                new Actor("Robin", "Wright"),
                new Actor("Gary", "Brando"),
                new Actor("Marlon", "Sinise"),
                new Actor("Al", "Pacino"),
                new Actor("James", "Caan"),
                new Actor("Christian", "Bale"),
                new Actor("Heath", "Ledger"),
                new Actor("Aaron", "Eckhart"),
                new Actor("Liam", "Neeson"),
                new Actor("Ralph", "Fiennes"),
                new Actor("Ben", "Kingsley"),
                new Actor("Leonardo", "DiCaprio"),
                new Actor("Joseph", "Gordon-Levitt"),
                new Actor("Elliot", "Page"),
            ];

            List<Customer> customers =
            [
                new Customer("Den", "Eme"),
                new Customer("Great", "Alexander"),
                new Customer("Emir", "Timur"),
                new Customer("Queen", "Cleopatra"),
                new Customer("Artemisia ", "Caria")
            ];

            List<MovieActor> movieActors =
            [
                new MovieActor(movies[0], actors[0]),
                new MovieActor(movies[0], actors[1]),
                new MovieActor(movies[0], actors[2]),
                new MovieActor(movies[1], actors[3]),
                new MovieActor(movies[1], actors[4]),
                new MovieActor(movies[1], actors[5]),
                new MovieActor(movies[2], actors[6]),
                new MovieActor(movies[2], actors[7]),
                new MovieActor(movies[2], actors[8]),
                new MovieActor(movies[3], actors[9]),
                new MovieActor(movies[3], actors[10]),
                new MovieActor(movies[3], actors[11]),
                new MovieActor(movies[4], actors[12]),
                new MovieActor(movies[4], actors[13]),
                new MovieActor(movies[4], actors[14])
            ];

            List<CustomerGenre> customerGenres =
            [
                new CustomerGenre(customers[0], genres[0]),
                new CustomerGenre(customers[0], genres[1]),
                new CustomerGenre(customers[0], genres[2]),
                new CustomerGenre(customers[0], genres[3]),
                new CustomerGenre(customers[0], genres[4]),
                new CustomerGenre(customers[1], genres[2]),
                new CustomerGenre(customers[1], genres[3]),
                new CustomerGenre(customers[2], genres[2]),
                new CustomerGenre(customers[3], genres[0]),
                new CustomerGenre(customers[3], genres[1]),
                new CustomerGenre(customers[4], genres[2]),
                new CustomerGenre(customers[4], genres[3]),
                new CustomerGenre(customers[4], genres[4]),
            ];

            List<Purchase> purchases =
            [
                new Purchase(movies[0].Price, DateTime.Now.AddDays(-1), customers[0], movies[0]),
                new Purchase(movies[1].Price, DateTime.Now.AddDays(-2), customers[0], movies[1]),
                new Purchase(movies[2].Price, DateTime.Now.AddDays(-3), customers[0], movies[2]),
                new Purchase(movies[3].Price, DateTime.Now.AddDays(-4), customers[0], movies[3]),
                new Purchase(movies[4].Price, DateTime.Now.AddDays(-5), customers[0], movies[4])
            ];

            context.AddRange(movies);
            context.AddRange(movieActors);
            context.AddRange(customerGenres);
            context.AddRange(purchases);

            context.SaveChanges();
        }
    }
}