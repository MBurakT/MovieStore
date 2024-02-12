using FluentValidation;
using WebApi.Dtos.GenreDtos.PutGenreDtos;

namespace WebApi.ControllerOperationValidators.GenreOperationValidators;

public class UpdateGenreDtoValidator : AbstractValidator<UpdateGenreDto>
{
    public UpdateGenreDtoValidator()
    {
        RuleFor(genre => genre).NotNull();
        RuleFor(genre => genre.Name).NotNull().NotEmpty();
    }
}