using Microsoft.AspNetCore.Mvc;
using WebApi.ControllerOperations.MovieOperations;
using WebApi.Dtos.MovieDtos.PostMovieDtos;
using WebApi.Dtos.MovieDtos.PutMovieDtos;

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

    [HttpPost]
    public IActionResult AddMovieCommand([FromBody] AddMovieDto addMovieDto)
    {
        _operation.AddMovieCommand(addMovieDto);
        return Ok();
    }

    [HttpPut("{id:int}")]
    public IActionResult UpdateMovieCommand(int id, [FromBody] UpdateMovieDto updateMovieDto)
    {
        _operation.UpdateMovieCommand(id, updateMovieDto);
        return Ok();
    }

    [HttpDelete("{id:int}")]
    public IActionResult DeleteMovieCommand(int id)
    {
        _operation.DeleteMovieCommand(id);
        return Ok();
    }
}