using FluentValidation;
using WebApi.Dtos.DirectorDtos.PostDirectorDtos;

namespace WebApi.ControllerOperationValidators.DirectorOperationValidators;

public class AddDirectorDtoValidator : AbstractValidator<AddDirectorDto>
{
    public AddDirectorDtoValidator()
    {
        RuleFor(director => director.Name).NotNull().NotEmpty();
        RuleFor(director => director.Surname).NotNull().NotEmpty();
    }
}