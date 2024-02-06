using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations.Configurations;
using WebApi.Entities;

namespace WebApi.DBOperations;

public class MovieStoreDbContext : DbContext, IMovieStoreDbContext
{
    public DbSet<Movie> Movies { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<Actor> Actors { get; set; }
    public DbSet<MovieActor> MovieActors { get; set; }
    public DbSet<Director> Directors { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Purchase> Purchases { get; set; }

    public override int SaveChanges()
    {
        return base.SaveChanges();
    }

    public MovieStoreDbContext(DbContextOptions<MovieStoreDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MovieConfiguration).Assembly);
        // new MovieConfiguration().Configure(modelBuilder.Entity<Movie>());
        // new ActorConfiguration().Configure(modelBuilder.Entity<Actor>());
        // new MovieActorConfiguration().Configure(modelBuilder.Entity<MovieActor>());
        // new GenreConfiguration().Configure(modelBuilder.Entity<Genre>());
        // new DirectorConfiguration().Configure(modelBuilder.Entity<Director>());
        // new CustomerConfiguration().Configure(modelBuilder.Entity<Customer>());
        // new CustomerGenreConfiguration().Configure(modelBuilder.Entity<CustomerGenre>());
        // new PurchaseConfiguration().Configure(modelBuilder.Entity<Purchase>());
    }
}