using FluentValidation;
using WebApi.Dtos.CustomerDtos.PostCustomerDtos;

namespace WebApi.ControllerOperationValidators.CustomerOperationValidators;

public class AddCustomerDtoValidator : AbstractValidator<AddCustomerDto>
{
    public AddCustomerDtoValidator()
    {
        RuleFor(x => x.Name).NotNull().NotEmpty();
        RuleFor(x => x.Surname).NotNull().NotEmpty();
    }
}