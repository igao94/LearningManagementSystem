using Application.Core;
using Application.Courses.DTOs;
using MediatR;

namespace Application.Courses.Queries.GetAllCourses;

public class GetAllCoursesQuery(CourseParams courseParams) : IRequest<Result<PagedList<CourseDto, DateTime?>>>
{
    public CourseParams CourseParams { get; set; } = courseParams;
}
