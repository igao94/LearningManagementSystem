using Application.Interfaces;
using Domain.Entities;
using FluentEmail.Core;

namespace Infrastructure.Services.EmailServices;

public class EmailSender(IFluentEmail emailSender) : IEmailSender
{
    public async Task SendConfirmationLinkAsync(User student, string verificationLink)
    {
        await emailSender
            .To(student.Email)
            .Subject("Email verification for LearningManagementSystem")
            .Body($"To verify your email <a href='{verificationLink}'>click here</a>", true)
            .SendAsync();
    }
}
