// dotnet watch run --project WebApi/WebApi.csproj
// dotnet ef migrations add InitialMigration --output-dir DBOperations/Migrations/InitialMigration
using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebApi.DBOperations;
using WebApi.DBOperations.DataSeeders;

namespace WebApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();

        // string conn = $"Data Source={Environment.CurrentDirectory}\\SQLite\\MovieStoreDB.db";
        string conn = $"Server={File.ReadAllText(Environment.CurrentDirectory + "\\Database.txt")};Database=MovieStoreDB;Trusted_Connection=True;TrustServerCertificate=True";
        builder.Services.AddDbContext<MovieStoreDbContext>(
            opt => opt.UseSqlServer(conn));
        // opt => opt.UseSqlite(conn));

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        #region SeedDatabase
        using (IServiceScope serviceScope = app.Services.CreateScope())
        {
            DataSeeder.Seed(serviceScope.ServiceProvider);
        }
        #endregion

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}