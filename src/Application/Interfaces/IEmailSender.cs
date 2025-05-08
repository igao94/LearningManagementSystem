using Domain.Entities;

namespace Application.Interfaces;

public interface IEmailSender
{
    Task SendConfirmationLinkAsync(string userEmail, string verificationLink);
}
