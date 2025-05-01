using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Config;

public class CourseConfiguration : IEntityTypeConfiguration<Course>
{
    public void Configure(EntityTypeBuilder<Course> builder)
    {
        builder.Property(c => c.Title).IsRequired();

        builder.Property(c => c.Description).IsRequired();

        builder.Property(c => c.InstructorName).IsRequired();

        builder.HasIndex(c => c.Title);

        builder.HasIndex(c => c.InstructorName);

        builder.HasIndex(c => c.CreatedAt);
    }
}
