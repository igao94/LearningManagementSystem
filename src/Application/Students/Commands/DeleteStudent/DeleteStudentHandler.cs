using Application.Core;
using Application.Interfaces;
using Domain.Interfaces;
using MediatR;

namespace Application.Students.Commands.DeleteStudent;

public class DeleteStudentHandler(IUnitOfWork unitOfWork,
    IUserAccessor userAccessor) : IRequestHandler<DeleteStudentCommand, Result<Unit>>
{
    public async Task<Result<Unit>> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
    {
        var student = await unitOfWork.StudentRepository
            .GetStudentWithCourseAttendancesAndLessonProgressesAndCertificatesByIdAsync(userAccessor.GetUserId());

        if (student is null)
        {
            return Result<Unit>.Failure("Student not found.", 404);
        }

        unitOfWork.CourseRepository.RemoveCertificates(student.Certificates);

        unitOfWork.CourseRepository.RemoveLessonProgresses(student.LessonProgresses);

        unitOfWork.StudentRepository.RemoveCourseAttendances(student.CourseAttendances);

        unitOfWork.StudentRepository.RemoveStudent(student);

        var result = await unitOfWork.SaveChangesAsync();

        return result
            ? Result<Unit>.Success(Unit.Value)
            : Result<Unit>.Failure("Failed to delete student.", 400);
    }
}
