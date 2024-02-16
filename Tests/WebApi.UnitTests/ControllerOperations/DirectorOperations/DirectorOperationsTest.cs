using System.Linq;
using AutoMapper;
using FluentAssertions;
using WebApi.ControllerOperations.DirectorOperations;
using WebApi.DBOperations;
using WebApi.Dtos.DirectorDtos.GetDirectorDtos;
using WebApi.Dtos.DirectorDtos.PostDirectorDtos;
using WebApi.Dtos.DirectorDtos.PutDirectorDtos;
using WebApi.Entities;
using WebApi.UnitTest.TestSetups;
using Xunit;

namespace WebApi.UnitTest.ControllerOperations.DirectorOperations;

public class DirectorOperationTest
{
    readonly IMovieStoreDbContext _context;
    readonly IMapper _mapper;
    readonly DirectorOperation _operation;

    public DirectorOperationTest()
    {
        _context = Setups.CreateContext();
        _mapper = Setups.CreateMapper();
        _operation = new(_context, _mapper);
    }

    [Fact]
    public void WhenGetDirectorQueryExecuted_GetDirectorsDto_ShouldBeReturn()
    {
        _operation.GetDirectorsQuery().Should().BeOfType<GetDirectorsDto>();
    }

    [Fact]
    public void WhenGetDirectorCommandExecuted_GetDirectorDto_ShouldBeReturn()
    {
        _operation.GetDirectorCommand(1).Should().BeOfType<GetDirectorDto>();
    }

    [Fact]
    public void WhenAddDirectorCommandExecuted_Director_ShouldBeAdded()
    {
        int count = _context.Directors.Count();

        AddDirectorDto addDirectorDto = new AddDirectorDto { Name = "Den", Surname = "Eme" };

        FluentActions.Invoking(() => _operation.AddDirectorCommand(addDirectorDto)).Invoke();

        _context.Directors.Count().Should().Be(count + 1);
    }

    [Fact]
    public void WhenUpdateDirectorCommandExecuted_Director_ShouldBeAdded()
    {
        int id = 1;

        Director director = _context.Directors.Single(x => x.Id == id);

        UpdateDirectorDto updateDirectorDto = new UpdateDirectorDto { Name = "Burak", Surname = "Turhan" };

        FluentActions.Invoking(() => _operation.UpdateDirectorCommand(id, updateDirectorDto)).Invoke();

        _context.Directors.Single(x => x.Id == id).Name.Should().NotBe(director.Name);
    }
}