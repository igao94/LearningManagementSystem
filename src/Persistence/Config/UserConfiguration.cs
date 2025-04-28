using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Config;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(u => u.FirstName).IsRequired();

        builder.Property(u => u.LastName).IsRequired();

        builder.Property(u => u.Email).IsRequired();

        builder.Property(u => u.UserName).IsRequired();

        builder.HasIndex(u => u.FirstName);

        builder.HasIndex(u => u.LastName);

        builder.HasIndex(u => u.Email);

        builder.HasIndex(u => u.UserName);
    }
}
