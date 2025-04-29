using Application.Core;
using MediatR;

namespace Application.Students.Commands.DeleteStudent;

public class DeleteStudentCommand : IRequest<Result<Unit>>
{

}
