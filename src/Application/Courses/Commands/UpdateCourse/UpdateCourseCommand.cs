using Application.Core;
using Application.Courses.DTOs;
using MediatR;

namespace Application.Courses.Commands.UpdateCourse;

public class UpdateCourseCommand(UpdateCourseDto updateCourseDto) : IRequest<Result<Unit>>
{
    public UpdateCourseDto UpdateCourseDto { get; set; } = updateCourseDto;
}
