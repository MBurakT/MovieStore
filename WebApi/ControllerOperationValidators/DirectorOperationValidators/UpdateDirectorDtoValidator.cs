using FluentValidation;
using WebApi.Dtos.DirectorDtos.PutDirectorDtos;

namespace WebApi.ControllerOperationValidators.DirectorOperationValidators;

public class UpdateDirectorDtoValidator : AbstractValidator<UpdateDirectorDto>
{
    public UpdateDirectorDtoValidator()
    {
        RuleFor(director => director.Name).NotNull().NotEmpty();
        RuleFor(director => director.Surname).NotNull().NotEmpty();
    }
}