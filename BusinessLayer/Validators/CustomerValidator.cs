using FluentValidation;
using BusinessLayer.Model;

namespace BusinessLayer.Validators;

public class CustomerValidator : AbstractValidator<CustomerDto>
{
    public CustomerValidator()
    {
        RuleFor(x => x.FullName).NotEmpty();
        RuleFor(x => x.FullName).NotNull();
        RuleFor(x => x.DateOfBirth).NotNull();
    }
}