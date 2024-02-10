using Microsoft.AspNetCore.Mvc;
using WebApi.ControllerOperations.DirectorOperations;

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
}