using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Persistence.Repositories;

public class EmailTokenRepository(AppDbContext context) : IEmailTokenRepository
{
    public async Task<EmailVerificationToken?> GetTokenWithStudentAsync(string tokenId)
    {
        return await context.EmailVerificationTokens
            .Include(t => t.Student)
            .FirstOrDefaultAsync(t => t.Id == tokenId);
    }

    public async Task RemoveExpiredTokensAsync()
    {
        var verficationTokens = await context.EmailVerificationTokens
            .Where(et => et.ExpiresAt < DateTime.UtcNow)
            .ToListAsync();

        context.EmailVerificationTokens.RemoveRange(verficationTokens);
    }

    public void RemoveToken(EmailVerificationToken token)
    {
        context.EmailVerificationTokens.Remove(token);
    }
}
