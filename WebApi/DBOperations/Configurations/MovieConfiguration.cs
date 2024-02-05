using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Entities;

namespace WebApi.DBOperations.Configurations;

public class MovieConfiguration : IEntityTypeConfiguration<Movie>
{
    public void Configure(EntityTypeBuilder<Movie> builder)
    {
        builder.ToTable("MOVIES");

        builder.Property(x => x.Id).UseIdentityColumn().HasColumnName("ID");
        builder.Property(x => x.Name).HasColumnName("NAME").IsRequired();
        builder.Property(x => x.ReleaseDate).HasColumnName("RELEASEDATE").IsRequired();
        builder.Property(x => x.Price).HasColumnName("PRICE").IsRequired();

        builder.HasOne(x => x.Genre).WithMany(x => x.Movies).HasForeignKey(x => x.GenreId);
        builder.HasOne(x => x.Director).WithMany(x => x.Movies).HasForeignKey(x => x.DirectorId);
    }
}