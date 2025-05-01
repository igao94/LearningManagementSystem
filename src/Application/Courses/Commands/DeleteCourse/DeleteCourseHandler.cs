using Application.Core;
using Domain.Interfaces;
using MediatR;

namespace Application.Courses.Commands.DeleteCourse;

public class DeleteCourseHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteCouresCommand, Result<Unit>>
{
    public async Task<Result<Unit>> Handle(DeleteCouresCommand request, CancellationToken cancellationToken)
    {
        var course = await unitOfWork.CourseRepository.GetCourseWithStudentsByIdAsync(request.Id);

        if (course is null)
        {
            return Result<Unit>.Failure("Course not found.", 404);
        }

        unitOfWork.StudentRepository.RemoveCourseAttendances(course.Attendees);

        unitOfWork.CourseRepository.RemoveCourse(course);

        var result = await unitOfWork.SaveChangesAsync();

        return result
            ? Result<Unit>.Success(Unit.Value)
            : Result<Unit>.Failure("Failed to delete course.", 400);
    }
}
