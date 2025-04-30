using Application.Core;
using MediatR;

namespace Application.Courses.Commands.DeleteCourse;

public class DeleteCouresCommand(string id) : IRequest<Result<Unit>>
{
    public string Id { get; set; } = id;
}
