using Application.Interfaces;
using Domain.Entities;
using FluentEmail.Core;

namespace Infrastructure.Services.EmailServices;

public class EmailSender(IFluentEmail email) : IEmailSender
{
    public async Task SendConfirmationLinkAsync(User student, string verificationLink)
    {
        await email
           .To(student.Email)
           .Subject("Email verification for LearningManagementSystem")
           .Body($"To verify your email <a href='{verificationLink}'>click here</a>", isHtml: true)
           .SendAsync();
    }
}
