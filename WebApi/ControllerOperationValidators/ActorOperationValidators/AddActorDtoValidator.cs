using FluentValidation;
using WebApi.Dtos.ActorDtos.PostActorDtos;

namespace WebApi.ControllerOperationValidators.ActorOperationValidators;

public class AddActorDtoValidator : AbstractValidator<AddActorDto>
{
    public AddActorDtoValidator()
    {
        RuleFor(actor => actor.Name).NotNull().NotEmpty();
        RuleFor(actor => actor.Surname).NotNull().NotEmpty();
    }
}