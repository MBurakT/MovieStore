using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Entities;

namespace WebApi.DBOperations.Configurations;

public class DirectorConfiguration : IEntityTypeConfiguration<Director>
{
    public void Configure(EntityTypeBuilder<Director> builder)
    {
        builder.Property(x => x.Id).UseIdentityColumn().HasColumnName("ID");
        builder.Property(x => x.Name).HasColumnName("NAME").IsRequired();
        builder.Property(x => x.Surname).HasColumnName("SURNAME").IsRequired();

        builder.ToTable("DIRECTORS");
    }
}