using Application.Core;
using Application.Courses.DTOs;
using Application.Interfaces;
using Domain.Interfaces;
using MediatR;

namespace Application.Courses.Queries.GetLessonsProgressByCourseId;

public class GetLessonsProgressByCourseIdHandler(IUnitOfWork unitOfWork,
    IUserAccessor userAccessor) : IRequestHandler<GetLessonsProgressByCourseIdQuery, Result<LessonProgressDto>>
{
    public async Task<Result<LessonProgressDto>> Handle(GetLessonsProgressByCourseIdQuery request,
        CancellationToken cancellationToken)
    {
        var studentId = userAccessor.GetUserId();

        var course = await unitOfWork.CourseRepository.GetCourseByIdAsync(request.Id);

        if (course is null)
        {
            return Result<LessonProgressDto>.Failure("Course not found.", 404);
        }

        var courseAttendance = await unitOfWork.StudentRepository.GetAttendanceByIdAsync(studentId, course.Id);

        if (courseAttendance is null)
        {
            return Result<LessonProgressDto>.Failure("You are not attending this course.", 400);
        }

        var totalLessons = await unitOfWork.CourseRepository.GetLessonsCountAsync(course.Id);

        var completedLessons = await unitOfWork.CourseRepository
            .GetCompletedLessonsCountForStudentAsync(studentId, course.Id);

        return Result<LessonProgressDto>.Success(new LessonProgressDto
        {
            CompletedLessons = completedLessons,
            TotalLessons = totalLessons
        });
    }
}
