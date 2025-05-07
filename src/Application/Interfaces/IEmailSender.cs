using Domain.Entities;

namespace Application.Interfaces;

public interface IEmailSender
{
    Task SendConfirmationLinkAsync(User student, string verificationLink);
}
