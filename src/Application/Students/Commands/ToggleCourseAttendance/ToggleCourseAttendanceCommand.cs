using Application.Core;
using MediatR;

namespace Application.Students.Commands.ToggleCourseAttendance;

public class ToggleCourseAttendanceCommand(string courseId) : IRequest<Result<Unit>>
{
    public string CourseId { get; set; } = courseId;
}
