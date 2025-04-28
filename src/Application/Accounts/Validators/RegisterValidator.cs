using Application.Accounts.Commands.Register;
using FluentValidation;

namespace Application.Accounts.Validators;

public class RegisterValidator : AbstractValidator<RegisterCommand>
{
    public RegisterValidator()
    {
        RuleFor(u => u.RegisterDto.FirstName)
            .NotEmpty().WithMessage("FirstName is required.")
            .Matches(@"^[A-Z][a-z]+$")
            .WithMessage("First name must start with an uppercase letter and contain only one word.");

        RuleFor(u => u.RegisterDto.LastName)
            .NotEmpty().WithMessage("LastName is required")
            .Matches(@"^[A-Z][a-z]+$")
            .WithMessage("Last name must start with an uppercase letter and contain only one word.");

        RuleFor(u => u.RegisterDto.Username)
            .NotEmpty().WithMessage("Username is required.")
            .Matches(@"^[a-z0-9]+$")
            .WithMessage("Username must be a single word with only lowercase letters and numbers, without spaces.");

        RuleFor(u => u.RegisterDto.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress()
            .WithMessage("Please enter a valid email address.");

        RuleFor(u => u.RegisterDto.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters.")
            .MaximumLength(20).WithMessage("Password cannot exceed 20 characters.")
            .Matches(@"[\W_]").WithMessage("Password must contain at least one non-alphanumeric character.")
            .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
            .Matches(@"\d").WithMessage("Password must contain at least one digit.");
    }
}
