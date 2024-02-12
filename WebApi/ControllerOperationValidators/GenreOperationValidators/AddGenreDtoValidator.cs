using FluentValidation;
using WebApi.Dtos.GenreDtos.PostGenreDtos;

namespace WebApi.ControllerOperationValidators.GenreOperationValidators;

public class AddGenreDtoValidator : AbstractValidator<AddGenreDto>
{
    public AddGenreDtoValidator()
    {
        RuleFor(genre => genre).NotNull();
        RuleFor(genre => genre.Name).NotNull().NotEmpty();
    }
}