using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using WebApi.ControllerOperations.MovieOperations;
using WebApi.DBOperations;
using WebApi.Dtos.MovieDtos.PostMovieDtos;
using WebApi.Dtos.MovieDtos.PutMovieDtos;
using WebApi.Entities;
using WebApi.Models.MovieDtos.GetMovieDtos;
using WebApi.UnitTest.TestSetups;
using Xunit;

namespace WebApi.UnitTest.ControllerOperations.MovieOperations;

public class MovieOperationTest
{
    readonly IMovieStoreDbContext _context;
    readonly IMapper _mapper;
    readonly MovieOperation _operation;

    public MovieOperationTest()
    {
        _context = Setups.CreateContext();
        _mapper = Setups.CreateMapper();
        _operation = new(_context, _mapper);
    }

    [Fact]
    public void WhenGetMoviesQueryExecuted_GetMoviesDto_ShouldBeReturn()
    {
        _operation.GetMoviesQuery().Should().BeOfType<GetMoviesDto>();
    }

    [Fact]
    public void WhenGetMoviesQueryExecuted_GetMoviesDtoMoviesCount_ShouldBeEqual()
    {
        int count = _context.Movies.AsNoTracking().Count(x => !x.IsDeleted);
        _operation.GetMoviesQuery().Movies?.Count().Should().Be(count);
    }

    [Fact]
    public void WhenGetMovieCommandByIdExecuted_GetMovieDto_ShouldBeReturn()
    {
        _operation.GetMovieCommandById(1).Should().BeOfType<GetMovieDto>();
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void WhenGetMovieCommandByIdExecutedWithInvalidId_InvalidOperationException_ShouldBeThrown(int id)
    {
        FluentActions.Invoking(() => _operation.GetMovieCommandById(id)).Should().Throw<InvalidOperationException>().WithMessage("Movie does not exist!");
    }

    [Fact]
    public void WhenAddMovieCommandExecutedWithExistingMovieName_InvalidOperationException_ShouldBeThrown()
    {
        AddMovieDto addMovieDto = new AddMovieDto
        {
            Name = "Inception",
            ReleaseDate = new DateTime(1994, 09, 14),
            Price = 110,
            Genre = new AddMovieDto.AddMovieGenreDto { Name = "Drama" },
            Director = new AddMovieDto.AddMovieDirectorDto { Name = "Quentin", Surname = "Tarantino" },
            Actors = [
                new AddMovieDto.AddMovieActorDto{Name = "John", Surname = "Travolta"},
                new AddMovieDto.AddMovieActorDto{Name = "Uma", Surname = "Thurman"},
                new AddMovieDto.AddMovieActorDto{Name = "Samuel L.", Surname = "Jackson"}
            ]
        };

        FluentActions.Invoking(() => _operation.AddMovieCommand(addMovieDto)).Should().Throw<InvalidOperationException>().WithMessage("Movie already exists!");
    }

    [Fact]
    public void WhenAddMovieCommandExecutedWithoutExistingGenreName_InvalidOperationException_ShouldBeThrown()
    {
        AddMovieDto addMovieDto = new AddMovieDto
        {
            Name = "Pulp Fiction",
            ReleaseDate = new DateTime(1994, 09, 14),
            Price = 110,
            Genre = new AddMovieDto.AddMovieGenreDto { Name = "Genre" },
            Director = new AddMovieDto.AddMovieDirectorDto { Name = "Quentin", Surname = "Tarantino" },
            Actors = [
                new AddMovieDto.AddMovieActorDto{Name = "John", Surname = "Travolta"},
                new AddMovieDto.AddMovieActorDto{Name = "Uma", Surname = "Thurman"},
                new AddMovieDto.AddMovieActorDto{Name = "Samuel L.", Surname = "Jackson"}
            ]
        };

        FluentActions.Invoking(() => _operation.AddMovieCommand(addMovieDto)).Should().Throw<InvalidOperationException>().WithMessage("Genre does not exist!");
    }

    [Fact]
    public void WhenAddMovieCommandExecutedWithValidAddMovieDto_Movie_ShouldBeAdded()
    {
        int movieCount = _context.Movies.Count();

        AddMovieDto addMovieDto = new AddMovieDto
        {
            Name = "Pulp Fiction",
            ReleaseDate = new DateTime(1994, 09, 14),
            Price = 110,
            Genre = new AddMovieDto.AddMovieGenreDto { Name = "Drama" },
            Director = new AddMovieDto.AddMovieDirectorDto { Name = "Quentin", Surname = "Tarantino" },
            Actors = [
                new AddMovieDto.AddMovieActorDto{Name = "John", Surname = "Travolta"},
                new AddMovieDto.AddMovieActorDto{Name = "Uma", Surname = "Thurman"},
                new AddMovieDto.AddMovieActorDto{Name = "Samuel L.", Surname = "Jackson"}
            ]
        };

        FluentActions.Invoking(() => _operation.AddMovieCommand(addMovieDto)).Invoke();

        _context.Movies.Count().Should().Be(movieCount + 1);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void WhenUpdateMovieCommandExecutedWithInvalidId_InvalidOperationException_ShouldBeThrown(int id)
    {
        UpdateMovieDto updateMovieDto = new UpdateMovieDto
        {
            Name = "Fight Club",
            ReleaseDate = new DateTime(1999, 09, 15),
            Price = 110,
            Genre = new UpdateMovieDto.UpdateMovieGenreDto { Name = "Crime" },
            Director = new UpdateMovieDto.UpdateMovieDirectorDto { Name = "Robert", Surname = "Zemeckis" },
            Actors = [
                new UpdateMovieDto.UpdateMovieActorDto{Name = "Tom", Surname = "Hanks"},
                new UpdateMovieDto.UpdateMovieActorDto{Name = "Robin", Surname = "Wright"},
                new UpdateMovieDto.UpdateMovieActorDto{Name = "Gary", Surname = "Brando"}
            ]
        };

        FluentActions.Invoking(() => _operation.UpdateMovieCommand(id, updateMovieDto)).Should().Throw<InvalidOperationException>().WithMessage("Movie does not exist!");
    }

    [Fact]
    public void WhenUpdateMovieCommandExecutedWithoutExistingMovieId_InvalidOperationException_ShouldBeThrown()
    {
        int id = _context.Movies.Count() + 10;

        UpdateMovieDto updateMovieDto = new UpdateMovieDto
        {
            Name = "Fight Club",
            ReleaseDate = new DateTime(1999, 09, 15),
            Price = 110,
            Genre = new UpdateMovieDto.UpdateMovieGenreDto { Name = "Crime" },
            Director = new UpdateMovieDto.UpdateMovieDirectorDto { Name = "Robert", Surname = "Zemeckis" },
            Actors = [
                new UpdateMovieDto.UpdateMovieActorDto{Name = "Tom", Surname = "Hanks"},
                new UpdateMovieDto.UpdateMovieActorDto{Name = "Robin", Surname = "Wright"},
                new UpdateMovieDto.UpdateMovieActorDto{Name = "Gary", Surname = "Brando"}
            ]
        };

        FluentActions.Invoking(() => _operation.UpdateMovieCommand(id, updateMovieDto)).Should().Throw<InvalidOperationException>().WithMessage("Movie does not exist!");
    }

    [Fact]
    public void WhenUpdateMovieCommandExecutedWithValidUpdateMovieDto_Movie_ShouldBeUpdated()
    {
        int id = 6;
        Movie movie = _context.Movies.AsNoTracking().Single(x => x.Id == id);

        UpdateMovieDto updateMovieDto = new UpdateMovieDto
        {
            Name = "Fight Club",
            ReleaseDate = new DateTime(1999, 09, 15),
            Price = 110,
            Genre = new UpdateMovieDto.UpdateMovieGenreDto { Name = "Crime" },
            Director = new UpdateMovieDto.UpdateMovieDirectorDto { Name = "Robert", Surname = "Zemeckis" },
            Actors = [
                new UpdateMovieDto.UpdateMovieActorDto{Name = "Tom", Surname = "Hanks"},
                new UpdateMovieDto.UpdateMovieActorDto{Name = "Robin", Surname = "Wright"},
                new UpdateMovieDto.UpdateMovieActorDto{Name = "Gary", Surname = "Brando"}
            ]
        };

        FluentActions.Invoking(() => _operation.UpdateMovieCommand(id, updateMovieDto)).Invoke();

        Movie updatedMovie = _context.Movies.AsNoTracking().Single(x => x.Id == id);

        updatedMovie.Name.Should().NotBe(movie.Name);
    }

    [Fact]
    public void WhenDeleteMovieCommandExecutedWithValidId_Movie_ShouldBeDeleted()
    {
        int id = 6, count = _context.Movies.AsNoTracking().Count(x => !x.IsDeleted);

        FluentActions.Invoking(() => _operation.DeleteMovieCommand(id)).Invoke();

        _context.Movies.AsNoTracking().Count(x => !x.IsDeleted).Should().Be(count - 1);
    }
}