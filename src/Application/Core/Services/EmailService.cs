using Application.Interfaces;
using Domain.Entities;

namespace Application.Core.Services;

public class EmailService(IEmailSender emailSender, IEmailLinkGenerator emailLinkGenerator) : IEmailService
{
    public string CreateAndAttachVerificationToken(User student)
    {
        var verificationToken = new EmailVerificationToken
        {
            StudentId = student.Id,
            CreatedOn = DateTime.UtcNow,
            ExpiresAt = DateTime.UtcNow.AddHours(1)
        };

        student.EmailVerificationTokens.Add(verificationToken);

        return verificationToken.Id;
    }

    public async Task<bool> SendConfirmationLinkAsync(string email, string verificationTokenId)
    {
        var verificationLink = emailLinkGenerator.CreateVerificationLink(verificationTokenId);

        return await emailSender.SendConfirmationLinkAsync(email, verificationLink);
    }

    public async Task<bool> SendResetPasswordTokenAsync(string email, string resetToken)
    {
        return await emailSender.SendResetPasswordTokenAsync(email, resetToken);
    }

    public async Task<bool> SendCourseLinkAsync(string studentEmail, Course course)
    {
        var courseLink = emailLinkGenerator.CreateCourseLink(course.Id);

        return await emailSender.SendCourseLinkAsync(studentEmail, course.Title, courseLink);
    }

    public async Task<bool> SendCompletedCourseNotificationAsync(string studentEmail, Course course)
    {
        var courseLink = emailLinkGenerator.CreateCourseLink(course.Id);

        return await emailSender.SendCompletedCourseNotificationAsync(studentEmail, course.Title, courseLink);
    }
}
