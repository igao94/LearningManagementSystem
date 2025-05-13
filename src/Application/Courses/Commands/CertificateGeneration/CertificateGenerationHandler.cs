using Application.Core;
using Application.Core.Services;
using Application.Courses.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Courses.Commands.CertificateGeneration;

public class CertificateGenerationHandler(IUnitOfWork unitOfWork,
    IUserAccessor userAccessor,
    IEmailService emailService,
    IMapper mapper) : IRequestHandler<CertificateGenerationCommand, Result<CertificateDto>>
{
    public async Task<Result<CertificateDto>> Handle(CertificateGenerationCommand request,
        CancellationToken cancellationToken)
    {
        var student = await unitOfWork.StudentRepository.GetStudentByIdAsync(userAccessor.GetUserId());

        if (student is null || student.Email is null)
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

        var certificateResult = await CreateCertificateAsync(student.Id, course.Id);

        if (certificateResult is null)
        {
            return Result<CertificateDto>.Failure("You already have certificate for this course.", 400);
        }

        var emailResult = await emailService.SendCompletedCourseNotificationAsync(student.Email, course);

        if (!emailResult)
        {
            return Result<CertificateDto>.Failure("Failed to send email.", 400);
        }

        var result = await unitOfWork.SaveChangesAsync();

        return result
            ? Result<CertificateDto>.Success(mapper.Map<CertificateDto>(certificateResult))
            : Result<CertificateDto>.Failure("Failed to generate certificate.", 400);
    }

    private async Task<bool> AreLessonsCompletedAsync(string courseId, string studentId)
    {
        var totalLessonCount = await unitOfWork.CourseRepository.GetLessonsCountAsync(courseId);

        var areLessonsCompleted = await unitOfWork.CourseRepository
            .AreLessonsCompletedByStudentAsync(courseId, studentId, totalLessonCount);

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
