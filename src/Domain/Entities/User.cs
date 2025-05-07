using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;

public class User : IdentityUser
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public ICollection<CourseAttendance> CourseAttendances { get; set; } = [];
    public ICollection<LessonProgress> LessonProgresses { get; set; } = [];
    public ICollection<Certificate> Certificates { get; set; } = [];
    public ICollection<EmailVerificationToken> EmailVerificationTokens { get; set; } = [];
}
