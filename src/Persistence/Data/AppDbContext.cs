using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Persistence.Config;

namespace Persistence.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext<User>(options)
{
    public DbSet<Course> Courses { get; set; }

    public DbSet<Lesson> Lessons { get; set; }

    public DbSet<CourseAttendance> CourseAttendances { get; set; }

    public DbSet<LessonProgress> LessonProgresses { get; set; }

    public DbSet<Certificate> Certificates { get; set; }

    public DbSet<EmailVerificationToken> EmailVerificationTokens { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(typeof(UserConfiguration).Assembly);
    }
}
