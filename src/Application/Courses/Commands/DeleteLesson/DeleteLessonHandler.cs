using Application.Core;
using Domain.Interfaces;
using MediatR;

namespace Application.Courses.Commands.DeleteLesson;

public class DeleteLessonHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteLessonCommand, Result<Unit>>
{
    public async Task<Result<Unit>> Handle(DeleteLessonCommand request, CancellationToken cancellationToken)
    {
        var lesson = await unitOfWork.CourseRepository.GetLessonWithProgressByIdAsync(request.Id);

        if (lesson is null)
        {
            return Result<Unit>.Failure("Lesson not found.", 404);
        }

        unitOfWork.CourseRepository.RemoveLessonProgresses(lesson.LessonProgresses);

        unitOfWork.CourseRepository.RemoveLesson(lesson);

        var result = await unitOfWork.SaveChangesAsync();

        return result
            ? Result<Unit>.Success(Unit.Value)
            : Result<Unit>.Failure("Failed to delete lesson.", 400);
    }
}
