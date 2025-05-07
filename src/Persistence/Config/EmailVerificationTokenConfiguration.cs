using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Config;

public class EmailVerificationTokenConfiguration : IEntityTypeConfiguration<EmailVerificationToken>
{
    public void Configure(EntityTypeBuilder<EmailVerificationToken> builder)
    {
        builder.HasOne(t => t.Student)
            .WithMany(u => u.EmailVerificationTokens)
            .HasForeignKey(t => t.StudentId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
