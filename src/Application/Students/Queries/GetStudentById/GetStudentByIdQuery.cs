using Application.Core;
using Application.Students.DTOs;
using MediatR;

namespace Application.Students.Queries.GetStudentById;

public class GetStudentByIdQuery(string id) : IRequest<Result<StudentDto>>
{
    public string Id { get; set; } = id;
}
