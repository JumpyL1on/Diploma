using FluentValidation;

namespace Backend.Application.Features.Account.Register;

public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(command => command.Email).EmailAddress();
        RuleFor(command => command.Password).NotNull();
        RuleFor(command => command.ConfirmPassword).Equal(command => command.Password);
    }
}