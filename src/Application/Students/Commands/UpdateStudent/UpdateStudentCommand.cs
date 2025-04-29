using Application.Core;
using Application.Students.DTOs;
using MediatR;

namespace Application.Students.Commands.UpdateStudent;

public class UpdateStudentCommand(UpdateStudentDto updateStudentDto) : IRequest<Result<Unit>>
{
    public UpdateStudentDto UpdateStudentDto { get; set; } = updateStudentDto;
}
