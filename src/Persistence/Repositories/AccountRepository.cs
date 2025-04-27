using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

public class AccountRepository(UserManager<User> userManager) : IAccountRepository
{
    public async Task<IdentityResult> AddToRoleAsync(User user, string role)
    {
        return await userManager.AddToRoleAsync(user, role);
    }

    public async Task<bool> EmailExistsAsync(string email)
    {
        return await userManager.Users.AnyAsync(u => u.Email == email);
    }

    public async Task<IdentityResult> RegisterUserAsync(User user, string password)
    {
        return await userManager.CreateAsync(user, password);
    }

    public async Task<bool> UsernameExistsAsync(string username)
    {
        return await userManager.Users.AnyAsync(u => u.UserName!.ToLower() == username);
    }
}
