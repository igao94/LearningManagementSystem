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

        var courseId = request.Id;

        var totalLessons = await unitOfWork.CourseRepository.GetLessonsCountAsync(courseId);

        var completedLessons = await unitOfWork.CourseRepository
            .GetCompletedLessonsCountForStudentAsync(studentId, courseId);

        return Result<LessonProgressDto>.Success(new LessonProgressDto
        {
            TotalLessons = totalLessons,
            CompletedLessons = completedLessons
        });
    }
}
