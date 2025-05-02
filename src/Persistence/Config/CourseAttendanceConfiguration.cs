using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Config;

public class CourseAttendanceConfiguration : IEntityTypeConfiguration<CourseAttendance>
{
    public void Configure(EntityTypeBuilder<CourseAttendance> builder)
    {
        builder.HasKey(ca => new { ca.StudentId, ca.CourseId });

        builder.HasOne(ca => ca.Student)
            .WithMany(u => u.CourseAttendances)
            .HasForeignKey(ca => ca.StudentId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(ca => ca.Course)
            .WithMany(c => c.Attendees)
            .HasForeignKey(ca => ca.CourseId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
