using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Entities;

namespace WebApi.DBOperations.Configurations;

public class ActorConfiguration : IEntityTypeConfiguration<Actor>
{
    public void Configure(EntityTypeBuilder<Actor> builder)
    {
        builder.Property(x => x.Id).UseIdentityColumn().HasColumnName("ID");
        builder.Property(x => x.Name).HasColumnName("NAME").IsRequired();
        builder.Property(x => x.Surname).HasColumnName("SURNAME").IsRequired();

        builder.ToTable("ACTORS");
    }
}