using Application.Core;
using Application.Interfaces;
using Application.Students.DTOs;
using AutoMapper;
using Domain.Interfaces;
using MediatR;

namespace Application.Students.Queries.GetStudentById;

public class GetStudentByIdHandler(IUnitOfWork unitOfWork,
    IUserAccessor userAccessor,
    IMapper mapper) : IRequestHandler<GetStudentByIdQuery, Result<StudentDto>>
{
    public async Task<Result<StudentDto>> Handle(GetStudentByIdQuery request,
        CancellationToken cancellationToken)
    {
        var currentUserId = userAccessor.GetUserId();

        var student = await unitOfWork.StudentRepository.GetStudentByIdAsync(request.Id, currentUserId);

        if (student is null)
        {
            return Result<StudentDto>.Failure("Student not found.", 404);
        }

        return Result<StudentDto>.Success(mapper.Map<StudentDto>(student));
    }
}
