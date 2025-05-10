using Application.Core;
using MediatR;

namespace Application.Students.Commands.AttendCourse;

public class AttendCourseCommand(string courseId) : IRequest<Result<Unit>>
{
    public string CourseId { get; set; } = courseId;
}
