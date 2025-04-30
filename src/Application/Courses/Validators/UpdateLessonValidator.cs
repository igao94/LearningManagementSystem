using Application.Courses.Commands.UpdateLesson;
using FluentValidation;

namespace Application.Courses.Validators;

public class UpdateLessonValidator : AbstractValidator<UpdateLessonCommand>
{
    public UpdateLessonValidator()
    {
        RuleFor(l => l.UpdateLessonDto.Title)
            .NotEmpty().WithMessage("Title is required.");

        RuleFor(l => l.UpdateLessonDto.ContentUrl)
            .NotEmpty().WithMessage("ContentUrl is required.");
    }
}
