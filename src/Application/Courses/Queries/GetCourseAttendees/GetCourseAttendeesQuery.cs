using Application.Core;
using Application.Students.DTOs;
using MediatR;

namespace Application.Courses.Queries.GetCourseAttendees;

public class GetCourseAttendeesQuery(string id) : IRequest<Result<IEnumerable<StudentDto>>>
{
    public string Id { get; set; } = id;
}
