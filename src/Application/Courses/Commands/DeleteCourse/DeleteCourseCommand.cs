using Application.Core;
using MediatR;

namespace Application.Courses.Commands.DeleteCourse;

public class DeleteCourseCommand(string id) : IRequest<Result<Unit>>
{
    public string Id { get; set; } = id;
}
