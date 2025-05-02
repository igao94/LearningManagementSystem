using Application.Core;
using MediatR;

namespace Application.Courses.Commands.TrackLessonProgress;

public class TrackLessonProgressCommand(string lessonId) : IRequest<Result<Unit>>
{
    public string LessonId { get; set; } = lessonId;
}
