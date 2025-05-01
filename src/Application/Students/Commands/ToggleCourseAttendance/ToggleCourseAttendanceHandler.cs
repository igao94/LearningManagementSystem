using Application.Core;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Students.Commands.ToggleCourseAttendance;

public class ToggleCourseAttendanceHandler(IUnitOfWork unitOfWork,
    IUserAccessor userAccessor) : IRequestHandler<ToggleCourseAttendanceCommand, Result<Unit>>
{
    public async Task<Result<Unit>> Handle(ToggleCourseAttendanceCommand request,
        CancellationToken cancellationToken)
    {
        var student = await unitOfWork.StudentRepository.GetStudentByIdAsync(userAccessor.GetUserId());

        if (student is null)
        {
            return Result<Unit>.Failure("Student not found.", 404);
        }

        var course = await unitOfWork.CourseRepository.GetCourseByIdAsync(request.CourseId);

        if (course is null)
        {
            return Result<Unit>.Failure("Course not found.", 404);
        }

        await AttendCourseAsync(student.Id, course.Id);

        var result = await unitOfWork.SaveChangesAsync();

        return result
            ? Result<Unit>.Success(Unit.Value)
            : Result<Unit>.Failure("Course attendance failed.", 400);
    }

    private async Task AttendCourseAsync(string studentId, string courseId)
    {
        var attendance = await unitOfWork.StudentRepository.GetAttendanceByIdAsync(studentId, courseId);

        if (attendance is null)
        {
            attendance = new CourseAttendance
            {
                StudentId = studentId,
                CourseId = courseId
            };

            unitOfWork.StudentRepository.AddCourseAttendance(attendance);
        }
        else
        {
            unitOfWork.StudentRepository.RemoveCourseAttendance(attendance);
        }
    }
}
