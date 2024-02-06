using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Entities;

namespace WebApi.DBOperations.Configurations;

public class CustomerGenreConfiguration : IEntityTypeConfiguration<CustomerGenre>
{
    public void Configure(EntityTypeBuilder<CustomerGenre> builder)
    {
        builder.HasKey(x => new { x.CustomerId, x.GenreId });

        builder.Property(x => x.CustomerId).HasColumnName("CUSTOMERID").IsRequired();
        builder.Property(x => x.GenreId).HasColumnName("GENREID").IsRequired();

        builder.HasOne(x => x.Customer).WithMany().HasForeignKey(x => x.CustomerId);
        builder.HasOne(x => x.Genre).WithMany().HasForeignKey(x => x.GenreId);

        builder.ToTable("CUSTOMERGENRES");
    }
}