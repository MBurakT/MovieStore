using Microsoft.AspNetCore.Mvc;
using WebApi.ControllerOperations.ActorOperations;
using WebApi.Dtos.ActorDtos.PostActorDtos;
using WebApi.Dtos.ActorDtos.PutActorDtos;

namespace WebApi.Controller;

[ApiController]
[Route("[controller]s")]
public class ActorController : ControllerBase
{
    readonly ActorOperation _operation;

    public ActorController(ActorOperation operation)
    {
        _operation = operation;
    }

    [HttpGet]
    public IActionResult GetActorsQuery()
    {
        return Ok(_operation.GetActorsQuery());
    }

    [HttpGet("{id:int}")]
    public IActionResult GetActorCommandById(int id)
    {
        return Ok(_operation.GetActorCommandById(id));
    }

    [HttpPost]
    public IActionResult AddActorCommand([FromBody] AddActorDto addActorDto)
    {
        _operation.AddActorCommand(addActorDto);
        return Ok();
    }

    [HttpPut("{id:int}")]
    public IActionResult UpdateActorCommand(int id, [FromBody] UpdateActorDto updateActorDto)
    {
        _operation.UpdateActorCommand(id, updateActorDto);
        return Ok();
    }
}