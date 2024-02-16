using System.Linq;
using AutoMapper;
using FluentAssertions;
using WebApi.ControllerOperations.ActorOperations;
using WebApi.DBOperations;
using WebApi.Dtos.ActorDtos.GetActorDtos;
using WebApi.Dtos.ActorDtos.PostActorDtos;
using WebApi.Dtos.ActorDtos.PutActorDtos;
using WebApi.Entities;
using WebApi.UnitTest.TestSetups;
using Xunit;

public class ActorOperationTest
{
    readonly IMovieStoreDbContext _context;
    readonly IMapper _mapper;
    readonly ActorOperation _operation;

    public ActorOperationTest()
    {
        _context = Setups.CreateContext();
        _mapper = Setups.CreateMapper();
        _operation = new(_context, _mapper);
    }

    [Fact]
    public void WhenGetActorQueryExecuted_GetActorsDto_ShouldBeReturn()
    {
        _operation.GetActorsQuery().Should().BeOfType<GetActorsDto>();
    }

    [Fact]
    public void WhenGetActorCommandByIdExecuted_GetActorDto_ShouldBeReturn()
    {
        _operation.GetActorCommandById(1).Should().BeOfType<GetActorDto>();
    }

    [Fact]
    public void WhenAddActorCommandExecuted_Actor_ShouldBeAdded()
    {
        int count = _context.Actors.Count();

        AddActorDto addActorDto = new AddActorDto { Name = "Den", Surname = "Eme" };

        FluentActions.Invoking(() => _operation.AddActorCommand(addActorDto)).Invoke();

        _context.Actors.Count().Should().Be(count + 1);
    }

    [Fact]
    public void WhenUpdateActorCommandExecuted_Actor_ShouldBeAdded()
    {
        int id = 1;

        Actor actor = _context.Actors.Single(x => x.Id == id);

        UpdateActorDto updateActorDto = new UpdateActorDto { Name = "Burak", Surname = "Turhan" };

        FluentActions.Invoking(() => _operation.UpdateActorCommand(id, updateActorDto)).Invoke();

        _context.Actors.Single(x => x.Id == id).Name.Should().NotBe(actor.Name);
    }
}