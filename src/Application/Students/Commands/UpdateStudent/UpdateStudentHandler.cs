using Application.Core;
using Application.Interfaces;
using AutoMapper;
using Domain.Interfaces;
using MediatR;

namespace Application.Students.Commands.UpdateStudent;

public class UpdateStudentHandler(IUnitOfWork unitOfWork,
    IUserAccessor userAccessor,
    IMapper mapper) : IRequestHandler<UpdateStudentCommand, Result<Unit>>
{
    public async Task<Result<Unit>> Handle(UpdateStudentCommand request, CancellationToken cancellationToken)
    {
        var student = await unitOfWork.StudentRepository.GetStudentByIdAsync(userAccessor.GetUserId());

        if (student is null)
        {
            return Result<Unit>.Failure("Student not found.", 404);
        }

        mapper.Map(request.UpdateStudentDto, student);

        var result = await unitOfWork.SaveChangesAsync();

        return result
            ? Result<Unit>.Success(Unit.Value)
            : Result<Unit>.Failure("Failed to update student.", 400);
    }
}
