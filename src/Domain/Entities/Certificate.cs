namespace Domain.Entities;

public class Certificate
{
    public string StudentId { get; set; } = null!;
    public User Student { get; set; } = null!;
    public string CourseId { get; set; } = null!;
    public Course Course { get; set; } = null!;
    public string CertificateUrl { get; set; } = null!;
    public DateTime IssuedAt { get; set; } = DateTime.UtcNow;
}
