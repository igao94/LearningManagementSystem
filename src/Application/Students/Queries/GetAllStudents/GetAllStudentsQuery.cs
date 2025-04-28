using Application.Core;
using Application.Students.DTOs;
using MediatR;

namespace Application.Students.Queries.GetAllStudents;

public class GetAllStudentsQuery(StudentParams studentParams) : IRequest<Result<IEnumerable<StudentDto>>>
{
    public StudentParams StudentParams { get; set; } = studentParams;
}
