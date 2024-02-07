using Microsoft.AspNetCore.Mvc;
using WebApi.ControllerOperations.MovieOperations;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]s")]
public class MovieController : ControllerBase
{
    readonly MovieOperation _operation;

    public MovieController(MovieOperation operation)
    {
        _operation = operation;
    }

    [HttpGet]
    public IActionResult GetMoviesQuery()
    {
        return Ok(_operation.GetMoviesQuery());
    }

    [HttpGet("{id:int}")]
    public IActionResult GetMovieCommandById(int id)
    {
        return Ok(_operation.GetMovieCommandById(id));
    }
}