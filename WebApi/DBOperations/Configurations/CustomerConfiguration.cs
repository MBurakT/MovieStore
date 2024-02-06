using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Entities;

namespace WebApi.DBOperations.Configurations;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.Property(x => x.Id).UseIdentityColumn().HasColumnName("ID");
        builder.Property(x => x.Name).HasColumnName("NAME").IsRequired();
        builder.Property(x => x.Surname).HasColumnName("SURNAME").IsRequired();

        builder.Ignore(x => x.Movies);
        builder.Ignore(x => x.FavoriteGenres);

        builder.ToTable("CUSTOMERS");
    }
}