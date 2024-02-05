﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApi.DBOperations;

#nullable disable

namespace WebApi.Migrations.InitialMigration
{
    [DbContext(typeof(MovieStoreDbContext))]
    partial class MovieStoreDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("WebApi.Entities.Actor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("NAME");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("SURNAME");

                    b.HasKey("Id");

                    b.ToTable("ACTORS", (string)null);
                });

            modelBuilder.Entity("WebApi.Entities.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("NAME");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("SURNAME");

                    b.HasKey("Id");

                    b.ToTable("CUSTOMERS", (string)null);
                });

            modelBuilder.Entity("WebApi.Entities.CustomerGenre", b =>
                {
                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<int>("GenreId")
                        .HasColumnType("int");

                    b.HasKey("CustomerId", "GenreId");

                    b.HasIndex("GenreId");

                    b.ToTable("CUSTOMERGENRES", (string)null);
                });

            modelBuilder.Entity("WebApi.Entities.Director", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("NAME");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("SURNAME");

                    b.HasKey("Id");

                    b.ToTable("DIRECTORS", (string)null);
                });

            modelBuilder.Entity("WebApi.Entities.Genre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("NAME");

                    b.HasKey("Id");

                    b.ToTable("GENRES", (string)null);
                });

            modelBuilder.Entity("WebApi.Entities.Movie", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("DirectorId")
                        .HasColumnType("int");

                    b.Property<int>("GenreId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("NAME");

                    b.Property<double>("Price")
                        .HasColumnType("float")
                        .HasColumnName("PRICE");

                    b.Property<DateTime>("ReleaseDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("RELEASEDATE");

                    b.HasKey("Id");

                    b.HasIndex("DirectorId");

                    b.HasIndex("GenreId");

                    b.ToTable("MOVIES", (string)null);
                });

            modelBuilder.Entity("WebApi.Entities.MovieActor", b =>
                {
                    b.Property<int>("MovieId")
                        .HasColumnType("int");

                    b.Property<int>("ActorId")
                        .HasColumnType("int");

                    b.HasKey("MovieId", "ActorId");

                    b.HasIndex("ActorId");

                    b.ToTable("MOVIEACTORS", (string)null);
                });

            modelBuilder.Entity("WebApi.Entities.Purchase", b =>
                {
                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<int>("MovieId")
                        .HasColumnType("int");

                    b.Property<double>("Price")
                        .HasColumnType("float")
                        .HasColumnName("PRICE");

                    b.Property<DateTime>("PurchaseTime")
                        .HasColumnType("datetime2")
                        .HasColumnName("PURCHASETIME");

                    b.HasKey("CustomerId", "MovieId");

                    b.HasIndex("MovieId");

                    b.ToTable("PURCHASES", (string)null);
                });

            modelBuilder.Entity("WebApi.Entities.CustomerGenre", b =>
                {
                    b.HasOne("WebApi.Entities.Customer", "Customer")
                        .WithMany("CustomerGenres")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApi.Entities.Genre", "Genre")
                        .WithMany("CustomerGenres")
                        .HasForeignKey("GenreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("Genre");
                });

            modelBuilder.Entity("WebApi.Entities.Movie", b =>
                {
                    b.HasOne("WebApi.Entities.Director", "Director")
                        .WithMany("Movies")
                        .HasForeignKey("DirectorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApi.Entities.Genre", "Genre")
                        .WithMany("Movies")
                        .HasForeignKey("GenreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Director");

                    b.Navigation("Genre");
                });

            modelBuilder.Entity("WebApi.Entities.MovieActor", b =>
                {
                    b.HasOne("WebApi.Entities.Actor", "Actor")
                        .WithMany("MovieActors")
                        .HasForeignKey("ActorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApi.Entities.Movie", "Movie")
                        .WithMany("MovieActors")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Actor");

                    b.Navigation("Movie");
                });

            modelBuilder.Entity("WebApi.Entities.Purchase", b =>
                {
                    b.HasOne("WebApi.Entities.Customer", "Customer")
                        .WithMany("Purchases")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApi.Entities.Movie", "Movie")
                        .WithMany("Purchases")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("Movie");
                });

            modelBuilder.Entity("WebApi.Entities.Actor", b =>
                {
                    b.Navigation("MovieActors");
                });

            modelBuilder.Entity("WebApi.Entities.Customer", b =>
                {
                    b.Navigation("CustomerGenres");

                    b.Navigation("Purchases");
                });

            modelBuilder.Entity("WebApi.Entities.Director", b =>
                {
                    b.Navigation("Movies");
                });

            modelBuilder.Entity("WebApi.Entities.Genre", b =>
                {
                    b.Navigation("CustomerGenres");

                    b.Navigation("Movies");
                });

            modelBuilder.Entity("WebApi.Entities.Movie", b =>
                {
                    b.Navigation("MovieActors");

                    b.Navigation("Purchases");
                });
#pragma warning restore 612, 618
        }
    }
}