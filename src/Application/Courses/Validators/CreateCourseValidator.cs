using Application.Courses.Commands.CreateCourse;
using FluentValidation;

namespace Application.Courses.Validators;

public class CreateCourseValidator : AbstractValidator<CreateCourseCommand>
{
    public CreateCourseValidator()
    {
        RuleFor(c => c.CreateCourseDto.Title)
            .NotEmpty().WithMessage("Title is required.");        
        
        RuleFor(c => c.CreateCourseDto.Description)
            .NotEmpty().WithMessage("Description is required.");        
        
        RuleFor(c => c.CreateCourseDto.InstructorName)
            .NotEmpty().WithMessage("InstructorName is required.");
    }
}
