using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Config;

public class CertificateConfiguration : IEntityTypeConfiguration<Certificate>
{
    public void Configure(EntityTypeBuilder<Certificate> builder)
    {
        builder.Property(ct => ct.CertificateUrl).IsRequired();

        builder.HasKey(ct => new { ct.StudentId, ct.CourseId });

        builder.HasOne(ct => ct.Student)
            .WithMany(u => u.Certificates)
            .HasForeignKey(ct => ct.StudentId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(ct => ct.Course)
            .WithMany(c => c.Certificates)
            .HasForeignKey(ct => ct.CourseId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
