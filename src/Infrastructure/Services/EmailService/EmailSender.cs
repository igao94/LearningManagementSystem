using Application.Interfaces;
using FluentEmail.Core;

namespace Infrastructure.Services.EmailService;

public class EmailSender(IFluentEmail emailSender) : IEmailSender
{
    public async Task SendConfirmationLinkAsync(string userEmail, string verificationLink)
    {
        await emailSender
            .To(userEmail)
            .Subject("Email verification for LearningManagementSystem")
            .Body($"To verify your email <a href='{verificationLink}'>click here</a>", true)
            .SendAsync();
    }

    public async Task SendCourseLinkAsync(string userEmail, string courseName, string courseLink)
    {
        await emailSender
            .To(userEmail)
            .Subject("Course attendance")
            .Body($"Thank you for joining course <a href='{courseLink}'>{courseName}</a>.", true)
            .SendAsync();
    }
}
