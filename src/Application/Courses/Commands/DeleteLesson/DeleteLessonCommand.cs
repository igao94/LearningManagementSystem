using Application.Core;
using MediatR;

namespace Application.Courses.Commands.DeleteLesson;

public class DeleteLessonCommand(string id) : IRequest<Result<Unit>>
{
    public string Id { get; set; } = id;
}
