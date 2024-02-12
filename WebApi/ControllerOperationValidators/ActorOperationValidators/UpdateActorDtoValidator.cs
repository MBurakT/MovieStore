using FluentValidation;
using WebApi.Dtos.ActorDtos.PutActorDtos;

namespace WebApi.ControllerOperationValidators.ActorOperationValidators;

public class UpdateActorDtoValidator : AbstractValidator<UpdateActorDto>
{
    public UpdateActorDtoValidator()
    {
        RuleFor(actor => actor.Name).NotNull().NotEmpty();
        RuleFor(actor => actor.Surname).NotNull().NotEmpty();
    }
}



