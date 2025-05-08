using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Domain.Interfaces;

public interface IAccountRepository
{
    Task<IdentityResult> RegisterUserAsync(User user, string password);
    Task<IdentityResult> AddToRoleAsync(User user, string role);
    Task<bool> EmailExistsAsync(string email);
    Task<bool> UsernameExistsAsync(string username);
    Task<User?> GetUserByEmailAsync(string email);
    Task<bool> CheckPasswordAsync(User user, string password);
    Task<User?> GetUserByIdAsync(string id);
    Task<string> GenerateResetPasswordTokenAsync(User user);
    Task<IdentityResult> ResetPasswordAsync(User user, string resetToken, string newPassword);
    Task<EmailVerificationToken?> GetTokenWithStudentAsync(string tokenId);
    void RemoveToken(EmailVerificationToken token);
    Task DeleteTokensForStudentAsync(string studentId);
}
