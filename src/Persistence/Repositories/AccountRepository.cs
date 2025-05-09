using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Persistence.Repositories;

public class AccountRepository(UserManager<User> userManager, AppDbContext context) : IAccountRepository
{
    public async Task<IdentityResult> AddToRoleAsync(User user, string role)
    {
        return await userManager.AddToRoleAsync(user, role);
    }

    public async Task<bool> EmailExistsAsync(string email)
    {
        return await userManager.Users.AnyAsync(u => u.Email == email);
    }

    public async Task<User?> GetUserByEmailAsync(string email)
    {
        return await userManager.FindByEmailAsync(email);
    }

    public async Task<User?> GetUserByEmailWithTokensAsync(string email)
    {
        return await context.Users
            .Include(u => u.EmailVerificationTokens)
            .FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<IdentityResult> RegisterUserAsync(User user, string password)
    {
        return await userManager.CreateAsync(user, password);
    }

    public async Task<bool> UsernameExistsAsync(string username)
    {
        return await userManager.Users.AnyAsync(u => u.UserName == username);
    }

    public async Task<bool> CheckPasswordAsync(User user, string password)
    {
        return await userManager.CheckPasswordAsync(user, password);
    }

    public async Task<User?> GetUserByIdAsync(string id)
    {
        return await userManager.FindByIdAsync(id);
    }

    public async Task<string> GenerateResetPasswordTokenAsync(User user)
    {
        return await userManager.GeneratePasswordResetTokenAsync(user);
    }

    public async Task<IdentityResult> ResetPasswordAsync(User user, string resetToken, string newPassword)
    {
        return await userManager.ResetPasswordAsync(user, resetToken, newPassword);
    }
}
