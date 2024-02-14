// dotnet watch run --project WebApi/WebApi.csproj
// dotnet ef migrations add InitialMigration --output-dir DBOperations/Migrations/InitialMigration
using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebApi.ControllerOperations.ActorOperations;
using WebApi.ControllerOperations.CustomerOperations;
using WebApi.ControllerOperations.DirectorOperations;
using WebApi.ControllerOperations.GenreOperations;
using WebApi.ControllerOperations.MovieOperations;
using WebApi.DBOperations;
using WebApi.DBOperations.DataSeeders;
using WebApi.Middlewares;
using WebApi.Services.LoggerServices;

namespace WebApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();

        // string conn = $"Data Source={Environment.CurrentDirectory}\\SQLite\\MovieStoreDB.db";
        string conn = $"Server={File.ReadAllText(Environment.CurrentDirectory + "\\Database.txt")};Database=MovieStoreDB;Trusted_Connection=True;TrustServerCertificate=True";
        builder.Services.AddDbContext<MovieStoreDbContext>(
            opt => opt.UseSqlServer(conn));
        // opt => opt.UseSqlite(conn));

        builder.Services.AddScoped<IMovieStoreDbContext>(provider => provider.GetRequiredService<MovieStoreDbContext>());

        builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

        builder.Services.AddScoped<MovieOperation>();
        builder.Services.AddScoped<GenreOperation>();
        builder.Services.AddScoped<DirectorOperation>();
        builder.Services.AddScoped<ActorOperation>();
        builder.Services.AddScoped<CustomerOperation>();

        builder.Services.AddSingleton<IFileLoggerService, FileLoggerService>();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        #region SeedDatabase
        // using (IServiceScope serviceScope = app.Services.CreateScope())
        // {
        //     DataSeeder.Seed(serviceScope.ServiceProvider);
        // }
        #endregion

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.UseCustomExceptionMiddleware();

        app.MapControllers();

        app.Run();
    }
}