using Application.Core;
using Domain.Interfaces;
using MediatR;

namespace Application.Courses.Commands.DeleteCourse;

public class DeleteCourseHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteCourseCommand, Result<Unit>>
{
    public async Task<Result<Unit>> Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
    {
        var course = await unitOfWork.CourseRepository
            .GetCourseWithAttendeesAndLessonsAndProgressAndCertificateByIdAsync(request.Id);

        if (course is null)
        {
            return Result<Unit>.Failure("Course not found.", 404);
        }

        var lessonProgresses = course.Lessons.SelectMany(l => l.LessonProgresses);

        unitOfWork.CourseRepository.RemoveLessonProgresses(lessonProgresses);

        unitOfWork.StudentRepository.RemoveCourseAttendances(course.Attendees);

        unitOfWork.CourseRepository.RemoveCertificates(course.Certificates);

        unitOfWork.CourseRepository.RemoveCourse(course);

        var result = await unitOfWork.SaveChangesAsync();

        return result
            ? Result<Unit>.Success(Unit.Value)
            : Result<Unit>.Failure("Failed to delete course.", 400);
    }
}
