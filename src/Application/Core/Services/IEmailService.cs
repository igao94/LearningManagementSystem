using Domain.Entities;

namespace Application.Core.Services;

public interface IEmailService
{
    string CreateAndAttachVerificationToken(User student);
    Task<bool> SendConfirmationLinkAsync(string email, string verificationTokenId);
    Task<bool> SendResetPasswordTokenAsync(string email, string resetToken);
    Task<bool> SendCourseLinkAsync(string studentEmail, Course course);
}
