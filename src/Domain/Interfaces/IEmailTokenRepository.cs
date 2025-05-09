using Domain.Entities;

namespace Domain.Interfaces;

public interface IEmailTokenRepository
{
    Task<EmailVerificationToken?> GetTokenWithStudentAsync(string tokenId);
    void RemoveToken(EmailVerificationToken token);
}
