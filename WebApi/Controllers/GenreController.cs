using Microsoft.AspNetCore.Mvc;
using WebApi.ControllerOperations.GenreOperations;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]s")]
public class GenreController : ControllerBase
{
    readonly GenreOperation _operation;

    public GenreController(GenreOperation operation)
    {
        _operation = operation;
    }

    [HttpGet]
    public IActionResult GetGenreQuery()
    {
        return Ok(_operation.GetGenreQuery());
    }

    [HttpGet("{id:int}")]
    public IActionResult GetGenreCommandById(int id)
    {
        return Ok(_operation.GetGenreCommandById(id));
    }
}