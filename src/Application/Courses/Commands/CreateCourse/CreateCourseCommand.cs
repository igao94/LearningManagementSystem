using Application.Core;
using Application.Courses.DTOs;
using MediatR;

namespace Application.Courses.Commands.CreateCourse;

public class CreateCourseCommand(CreateCourseDto createCourseDto) : IRequest<Result<CourseDto>>
{
    public CreateCourseDto CreateCourseDto { get; set; } = createCourseDto;
}
