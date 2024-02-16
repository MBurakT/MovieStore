using System.Linq;
using AutoMapper;
using FluentAssertions;
using WebApi.ControllerOperations.GenreOperations;
using WebApi.DBOperations;
using WebApi.Dtos.GenreDtos;
using WebApi.Dtos.GenreDtos.PostGenreDtos;
using WebApi.Dtos.GenreDtos.PutGenreDtos;
using WebApi.Entities;
using WebApi.UnitTest.TestSetups;
using Xunit;

namespace WebApi.UnitTest.ControllerOperations.GenreOperations;

public class GenreOperationTest
{
    readonly IMovieStoreDbContext _context;
    readonly IMapper _mapper;
    readonly GenreOperation _operation;

    public GenreOperationTest()
    {
        _context = Setups.CreateContext();
        _mapper = Setups.CreateMapper();
        _operation = new(_context, _mapper);
    }

    [Fact]
    public void WhenGetGenreQueryExecuted_GetGenresDto_ShouldBeReturn()
    {
        _operation.GetGenreQuery().Should().BeOfType<GetGenresDto>();
    }

    [Fact]
    public void WhenGetGenreCommandByIdExecuted_GetGenreDto_ShouldBeReturn()
    {
        _operation.GetGenreCommandById(1).Should().BeOfType<GetGenresDto>();
    }

    [Fact]
    public void WhenAddGenreCommandExecuted_Genre_ShouldBeAdded()
    {
        int count = _context.Genres.Count();

        AddGenreDto addGenreDto = new AddGenreDto { Name = "Adventure" };

        FluentActions.Invoking(() => _operation.AddGenreCommand(addGenreDto)).Invoke();

        _context.Genres.Count().Should().Be(count + 1);
    }

    [Fact]
    public void WhenUpdateGenreCommandExecuted_Genre_ShouldBeAdded()
    {
        int id = 1;

        Genre genre = _context.Genres.Single(x => x.Id == id);

        UpdateGenreDto updateGenreDto = new UpdateGenreDto { Name = "Adventure" };

        FluentActions.Invoking(() => _operation.UpdateGenreCommand(id, updateGenreDto)).Invoke();

        _context.Genres.Single(x => x.Id == id).Name.Should().NotBe(genre.Name);
    }
}