using Application.Students.Commands.UpdateStudent;
using FluentValidation;

namespace Application.Students.Validators;

public class UpdateStudentValidator : AbstractValidator<UpdateStudentCommand>
{
    public UpdateStudentValidator()
    {
        RuleFor(u => u.UpdateStudentDto.Username)
            .NotEmpty().WithMessage("Username is required.")
            .Matches(@"^[a-z0-9]+$")
            .WithMessage("Username must be a single word with only lowercase letters and numbers, without spaces.");

        RuleFor(u => u.UpdateStudentDto.FirstName)
            .NotEmpty().WithMessage("FirstName is required.")
            .Matches(@"^[A-Z][a-z]+$")
            .WithMessage("First name must start with an uppercase letter and contain only one word.");

        RuleFor(u => u.UpdateStudentDto.LastName)
            .NotEmpty().WithMessage("LastName is required")
            .Matches(@"^[A-Z][a-z]+$")
            .WithMessage("Last name must start with an uppercase letter and contain only one word.");
    }
}
