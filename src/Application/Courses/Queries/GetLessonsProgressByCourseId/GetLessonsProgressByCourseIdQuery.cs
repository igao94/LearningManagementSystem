using Application.Core;
using Application.Courses.DTOs;
using MediatR;

namespace Application.Courses.Queries.GetLessonsProgressByCourseId;

public class GetLessonsProgressByCourseIdQuery(string id) : IRequest<Result<LessonProgressDto>>
{
    public string Id { get; set; } = id;
}
