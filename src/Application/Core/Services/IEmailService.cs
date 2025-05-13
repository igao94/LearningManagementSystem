using Domain.Entities;

namespace Application.Core.Services;

public interface IEmailService
{
    string CreateAndAttachVerificationToken(User student);
    Task<bool> SendConfirmationLinkAsync(string email, string verificationTokenId);
}
