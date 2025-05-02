using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Config;

public class LessonProgresConfiguration : IEntityTypeConfiguration<LessonProgress>
{
    public void Configure(EntityTypeBuilder<LessonProgress> builder)
    {
        builder.HasKey(lp => new { lp.StudentId, lp.LessonId });

        builder.HasOne(lp => lp.Student)
            .WithMany(u => u.LessonProgresses)
            .HasForeignKey(lp => lp.StudentId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(lp => lp.Lesson)
            .WithMany(l => l.LessonProgresses)
            .HasForeignKey(lp => lp.LessonId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
