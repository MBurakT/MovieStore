using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Entities;

namespace WebApi.DBOperations.Configurations;

public class MovieActorConfiguration : IEntityTypeConfiguration<MovieActor>
{
    public void Configure(EntityTypeBuilder<MovieActor> builder)
    {
        builder.HasKey(x => new { x.MovieId, x.ActorId });

        builder.Property(x => x.MovieId).HasColumnName("MOVIEID").IsRequired();
        builder.Property(x => x.ActorId).HasColumnName("ACTORID").IsRequired();

        builder.HasOne(x => x.Movie).WithMany().HasForeignKey(x => x.MovieId);
        builder.HasOne(x => x.Actor).WithMany().HasForeignKey(x => x.ActorId);

        builder.ToTable("MOVIEACTORS");
    }
}