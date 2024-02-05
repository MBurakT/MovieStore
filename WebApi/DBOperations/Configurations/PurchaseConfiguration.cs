using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Entities;

namespace WebApi.DBOperations.Configurations;

public class PurchaseConfiguration : IEntityTypeConfiguration<Purchase>
{
    public void Configure(EntityTypeBuilder<Purchase> builder)
    {
        builder.ToTable("PURCHASES");

        builder.HasKey(x => new { x.CustomerId, x.MovieId });
        builder.Property(x => x.Price).HasColumnName("PRICE").IsRequired();
        builder.Property(x => x.PurchaseTime).HasColumnName("PURCHASETIME").IsRequired();

        builder.HasOne(x => x.Customer).WithMany(x => x.Purchases).HasForeignKey(x => x.CustomerId);
        builder.HasOne(x => x.Movie).WithMany(x => x.Purchases).HasForeignKey(x => x.MovieId);
    }
}