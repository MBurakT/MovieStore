using FluentValidation;
using WebApi.Dtos.CustomerDtos.PutCustomerDtos;

namespace WebApi.ControllerOperationValidators.CustomerOperationValidators;

public class UpdateCustomerDtoValidator : AbstractValidator<UpdateCustomerDto>
{
    public UpdateCustomerDtoValidator()
    {
        RuleFor(x => x.Name).NotNull().NotEmpty();
        RuleFor(x => x.Surname).NotNull().NotEmpty();
    }
}