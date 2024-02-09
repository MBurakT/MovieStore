using Microsoft.AspNetCore.Mvc;
using WebApi.ControllerOperations.GenreOperations;
using WebApi.Dtos.GenreDtos.PostGenreDtos;
using WebApi.Dtos.GenreDtos.PutGenreDtos;

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

    [HttpPost]
    public IActionResult AddGenreCommand([FromBody] AddGenreDto addGenreDto)
    {
        _operation.AddGenreCommand(addGenreDto);
        return Ok();
    }

    [HttpPut("{id:int}")]
    public IActionResult UpdateGenreCommand(int id, [FromBody] UpdateGenreDto updateGenreDto)
    {
        _operation.UpdateGenreCommand(id, updateGenreDto);
        return Ok();
    }
}