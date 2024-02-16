using System;
using System.IO;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;
using WebApi.MappingProfiles;

namespace WebApi.UnitTest.TestSetups;

public class Setups
{
    public static IMovieStoreDbContext CreateContext()
    {
        string dbName = File.ReadAllText(Environment.CurrentDirectory + "\\..\\..\\..\\TestSetups\\Database.txt");
        string conn = $"Server={dbName};Database=MovieStoreTestDB;Trusted_Connection=True;TrustServerCertificate=True";
        return new MovieStoreDbContext(new DbContextOptionsBuilder<MovieStoreDbContext>().UseSqlServer(conn).Options);
    }

    public static IMapper CreateMapper()
    {
        return new MapperConfiguration(config =>
        {
            config.AddMaps(typeof(MovieProfile).Assembly);
        }).CreateMapper();
    }
}