using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Entities;

namespace WebApi.DBOperations.Configurations;

public class GenreConfiguration : IEntityTypeConfiguration<Genre>
{
    public void Configure(EntityTypeBuilder<Genre> builder)
    {
        builder.ToTable("GENRES");

        builder.Property(x => x.Id).UseIdentityColumn().HasColumnName("ID");
        builder.Property(x => x.Name).HasColumnName("NAME").IsRequired();
    }
}