using Application.Accounts.Commands.ResetPassword;
using FluentValidation;

namespace Application.Accounts.Validators;

public class ResetPasswordValidator : AbstractValidator<ResetPasswordCommand>
{
    public ResetPasswordValidator()
    {
        RuleFor(rp => rp.ResetPasswordDto.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress()
            .Matches(@"^[^@]+@([a-zA-Z0-9-]+\.)+[a-zA-Z]{2,}$")
            .WithMessage("Please enter a valid email address.");

        RuleFor(rp => rp.ResetPasswordDto.NewPassword)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters.")
            .MaximumLength(20).WithMessage("Password cannot exceed 20 characters.")
            .Matches(@"[\W_]").WithMessage("Password must contain at least one non-alphanumeric character.")
            .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
            .Matches(@"\d").WithMessage("Password must contain at least one digit.");

        RuleFor(rp => rp.ResetPasswordDto.ResetToken)
            .NotEmpty().WithMessage("ResetToken is required.");
    }
}
