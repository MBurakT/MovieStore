using Microsoft.AspNetCore.Mvc;
using WebApi.ControllerOperations.DirectorOperations;
using WebApi.Dtos.DirectorDtos.PostDirectorDtos;
using WebApi.Dtos.DirectorDtos.PutDirectorDtos;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]s")]
public class DirectorController : ControllerBase
{
    readonly DirectorOperation _operation;

    public DirectorController(DirectorOperation operation)
    {
        _operation = operation;
    }

    [HttpGet]
    public IActionResult GetDirectorsQuery()
    {
        return Ok(_operation.GetDirectorsQuery());
    }

    [HttpGet("{id:int}")]
    public IActionResult GetDirectorCommand(int id)
    {
        return Ok(_operation.GetDirectorCommand(id));
    }

    [HttpPost]
    public IActionResult AddDirectorCommand([FromBody] AddDirectorDto addDirectorDto)
    {
        _operation.AddDirectorCommand(addDirectorDto);
        return Ok();
    }

    [HttpPut("{id:int}")]
    public IActionResult UpdateDirectorCommand(int id, [FromBody] UpdateDirectorDto updateDirectorDto)
    {
        _operation.UpdateDirectorCommand(id, updateDirectorDto);
        return Ok();
    }
}