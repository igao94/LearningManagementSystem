using Application.Courses.Commands.UpdateCourse;
using FluentValidation;

namespace Application.Courses.Validators;

public class UpdateCourseValidator : AbstractValidator<UpdateCourseCommand>
{
    public UpdateCourseValidator()
    {
        RuleFor(c => c.UpdateCourseDto.Title)
            .NotEmpty().WithMessage("Title is required.");

        RuleFor(c => c.UpdateCourseDto.Description)
            .NotEmpty().WithMessage("Description is required.");

        RuleFor(c => c.UpdateCourseDto.InstructorName)
            .NotEmpty().WithMessage("InstructorName is required.");
    }
}
