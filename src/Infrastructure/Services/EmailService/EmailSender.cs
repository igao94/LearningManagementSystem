using Application.Interfaces;
using FluentEmail.Core;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services.EmailService;

public class EmailSender(IFluentEmail emailSender, ILogger<EmailSender> logger) : IEmailSender
{
    public async Task<bool> SendConfirmationLinkAsync(string userEmail, string verificationLink)
    {
        try
        {
            var result = await emailSender
                .To(userEmail)
                .Subject("Email verification for LearningManagementSystem")
                .Body($"To verify your email <a href='{verificationLink}'>click here</a>", true)
                .SendAsync();

            return result.Successful;

        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message);
        }

        return false;
    }

    public async Task<bool> SendCourseLinkAsync(string userEmail, string courseName, string courseLink)
    {
        try
        {
            var result = await emailSender
                .To(userEmail)
                .Subject("Course attendance")
                .Body($"Thank you for joining course <a href='{courseLink}'>{courseName}</a>.", true)
                .SendAsync();

            return result.Successful;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);
        }

        return false;
    }

    public async Task<bool> SendResetPasswordTokenAsync(string userEmail, string resetToken)
    {
        try
        {
            var result = await emailSender
                .To(userEmail)
                .Subject("Reset password request")
                .Body($"{resetToken}", true)
                .SendAsync();

            return result.Successful;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);
        }

        return false;
    }
}
