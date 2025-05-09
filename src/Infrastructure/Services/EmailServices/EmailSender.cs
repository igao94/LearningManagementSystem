using Application.Interfaces;
using FluentEmail.Core;

namespace Infrastructure.Services.EmailServices;

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
}
