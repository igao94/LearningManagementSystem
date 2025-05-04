using Application.Core;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Courses.Commands.TrackLessonProgress;

public class TrackLessonProgressHandler(IUnitOfWork unitOfWork,
    IUserAccessor userAccessor) : IRequestHandler<TrackLessonProgressCommand, Result<Unit>>
{
    public async Task<Result<Unit>> Handle(TrackLessonProgressCommand request, CancellationToken cancellationToken)
    {
        var studentId = userAccessor.GetUserId();

        var lesson = await unitOfWork.CourseRepository.GetLessonByIdAsync(request.LessonId);

        if (lesson is null)
        {
            return Result<Unit>.Failure("Lesson not found.", 404);
        }

        var attendance = await unitOfWork.StudentRepository.GetAttendanceByIdAsync(studentId, lesson.CourseId);

        if (attendance is null)
        {
            return Result<Unit>.Failure("You must attend course to complete lesson.", 400);
        }

        var lessonProgress = await MarkLessonAsCompletedAsync(studentId, lesson.Id);

        if (!lessonProgress) return Result<Unit>.Failure("You have already completed this lesson.", 400);

        var result = await unitOfWork.SaveChangesAsync();

        return result
            ? Result<Unit>.Success(Unit.Value)
            : Result<Unit>.Failure("Failed to mark lesson as completed.", 400);
    }

    private async Task<bool> MarkLessonAsCompletedAsync(string studentId, string lessonId)
    {
        var lessonProgress = await unitOfWork.CourseRepository.GetLessonProgressAsync(studentId, lessonId);

        if (lessonProgress is null)
        {
            lessonProgress = new LessonProgress
            {
                StudentId = studentId,
                LessonId = lessonId,
                IsCompleted = true
            };

            unitOfWork.CourseRepository.AddLessonProgress(lessonProgress);

            return true;
        }

        return false;
    }
}
