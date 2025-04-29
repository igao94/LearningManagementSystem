using Application.Core;
using Application.Courses.DTOs;
using MediatR;

namespace Application.Courses.Queries.GetAllCourses;

public class GetAllCoursesQuery(CourseParams courseParams) : IRequest<Result<IEnumerable<CourseDto>>>
{
    public CourseParams CourseParams { get; set; } = courseParams;
}
