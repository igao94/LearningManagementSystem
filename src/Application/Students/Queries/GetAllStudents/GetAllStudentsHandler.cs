using Application.Core;
using Application.Interfaces;
using Application.Students.DTOs;
using AutoMapper;
using Domain.Interfaces;
using MediatR;

namespace Application.Students.Queries.GetAllStudents;

public class GetAllStudentsHandler(IUnitOfWork unitOfWork,
    IUserAccessor userAccessor,
    IMapper mapper) : IRequestHandler<GetAllStudentsQuery, Result<IEnumerable<StudentDto>>>
{
    public async Task<Result<IEnumerable<StudentDto>>> Handle(GetAllStudentsQuery request,
        CancellationToken cancellationToken)
    {
        var id = userAccessor.GetUserId();

        var students = await unitOfWork.StudentRepository.GetAllStudentsAsync(id, request.StudentParams.Search);

        return Result<IEnumerable<StudentDto>>.Success(mapper.Map<IEnumerable<StudentDto>>(students));
    }
}
