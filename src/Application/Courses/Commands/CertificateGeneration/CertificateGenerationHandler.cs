using Application.Core;
using Application.Courses.DTOs;
using Application.Interfaces;
using Application.Students.DTOs;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Courses.Commands.CertificateGeneration;

public class CertificateGenerationHandler(IUnitOfWork unitOfWork,
    IUserAccessor userAccessor,
    IMapper mapper) : IRequestHandler<CertificateGenerationCommand, Result<CertificateDto>>
{
    public async Task<Result<CertificateDto>> Handle(CertificateGenerationCommand request,
        CancellationToken cancellationToken)
    {
        var student = await unitOfWork.StudentRepository.GetStudentByIdAsync(userAccessor.GetUserId());

        if (student is null)
        {
            return Result<CertificateDto>.Failure("Student not found.", 404);
        }

        var course = await unitOfWork.CourseRepository.GetCourseByIdAsync(request.Id);

        if (course is null)
        {
            return Result<CertificateDto>.Failure("Course not found.", 404);
        }

        var areLessonsCompleted = await AreLessonsCompletedAsync(course.Id, student.Id);

        if (!areLessonsCompleted)
        {
            return Result<CertificateDto>.Failure("You need to complete all lessons to get certificate.", 400);
        }

        var certificate = await CreateCertificateAsync(student.Id, course.Id);

        if (certificate is null)
        {
            return Result<CertificateDto>.Failure("You already have certificate for this course.", 400);
        }

        var result = await unitOfWork.SaveChangesAsync();

        return result
            ? Result<CertificateDto>.Success(mapper.Map<CertificateDto>(certificate))
            : Result<CertificateDto>.Failure("Failed to generate certificate.", 400);
    }

    private async Task<bool> AreLessonsCompletedAsync(string courseId, string studentId)
    {
        var areLessonsCompleted = await unitOfWork.CourseRepository
            .AreLessonsCompletedByStudentAsync(courseId, studentId);

        if (!areLessonsCompleted)
        {
            return false;
        }

        return true;
    }

    private async Task<Certificate?> CreateCertificateAsync(string studentId, string courseId)
    {
        var certificate = await unitOfWork.CourseRepository.GetCertificateByIdAsync(studentId, courseId);

        if (certificate is null)
        {
            certificate = new Certificate
            {
                CourseId = courseId,
                StudentId = studentId,
                CertificateUrl = "https://example.com"
            };

            unitOfWork.CourseRepository.AddCertificate(certificate);

            return certificate;
        }

        return null;
    }
}
