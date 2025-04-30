using Application.Courses.Commands.CreateLesson;
using FluentValidation;

namespace Application.Courses.Validators;

public class CreateLessonValidator : AbstractValidator<CreateLessonCommand>
{
    public CreateLessonValidator()
    {
        RuleFor(l => l.CreateLessonDto.CourseId)
            .NotEmpty().WithMessage("CourseId is required.");

        RuleFor(l => l.CreateLessonDto.Title)
            .NotEmpty().WithMessage("Title is required.");

        RuleFor(l => l.CreateLessonDto.ContentUrl)
            .NotEmpty().WithMessage("ContentUrl is required.");
    }
}
