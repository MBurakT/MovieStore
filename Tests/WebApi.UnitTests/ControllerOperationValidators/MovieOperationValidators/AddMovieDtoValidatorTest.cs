using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using FluentAssertions;
using WebApi.ControllerOperationValidators.MovieOperationValidators;
using WebApi.Dtos.MovieDtos.PostMovieDtos;
using Xunit;

namespace Test.WebApi.UnitTests.ControllerOperationValidators.MovieOperationValidators;

public class AddMovieDtoValidatorTest
{
    readonly AddMovieDtoValidator _validator;

    public AddMovieDtoValidatorTest()
    {
        _validator = new AddMovieDtoValidator();
    }

    [Theory]
    [InlineData(null, 1994, 9, 14, "Drama", "Quentin", "Tarantino", "John;Travolta,Uma;Thurman,Samuel L.;Jackson")]
    [InlineData("Pulp Fiction", null, null, null, "Drama", "Quentin", "Tarantino", "John;Travolta,Uma;Thurman,Samuel L.;Jackson")]
    [InlineData("Pulp Fiction", 1994, 9, 14, null, "Quentin", "Tarantino", "John;Travolta,Uma;Thurman,Samuel L.;Jackson")]
    [InlineData("Pulp Fiction", 1994, 9, 14, "Drama", null, null, "John;Travolta,Uma;Thurman,Samuel L.;Jackson")]
    [InlineData("Pulp Fiction", 1994, 9, 14, "Drama", "Quentin", "Tarantino", "John;Travolta,Uma;Thurman,Samuel L.;Jackson")]
    [InlineData("Pulp Fiction", 1994, 9, 14, "Drama", "Quentin", "Tarantino", null)]
    public void WhenAnInvalidPropertyOfAddMovieDtoisGiven_ErrorCount_ShouldBeGreaterThanZero(
        string? name, int? year, int? month, int? day, string? genre, string directorName, string directorSurname, string? actor)
    {
        List<AddMovieDto.AddMovieActorDto>? actors = null;

        if (actor is not null)
        {
            actors = new();

            foreach (string act in actor.Split(","))
            {
                string[] s = act.Split(";");
                actors.Add(new AddMovieDto.AddMovieActorDto { Name = s[0], Surname = s[1] });
            }
        }

        AddMovieDto addMovieDto = new AddMovieDto
        {
            Name = name,
            ReleaseDate = year is not null ? new DateTime(year.Value, month.Value, day.Value) : null,
            Price = 110,
            Genre = new AddMovieDto.AddMovieGenreDto { Name = genre },
            Director = new AddMovieDto.AddMovieDirectorDto { Name = directorName, Surname = directorSurname },
            Actors = actors
        };

        var result = _validator.Validate(addMovieDto);

        result.Errors.Count.Should().BeGreaterThan(0);
    }
}