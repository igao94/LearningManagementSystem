using Application.Core;
using Application.Courses.DTOs;
using MediatR;

namespace Application.Courses.Queries.GetCourseById;

public class GetCourseByIdQuery(string id) : IRequest<Result<CourseDto>>
{
    public string Id { get; set; } = id;
}
