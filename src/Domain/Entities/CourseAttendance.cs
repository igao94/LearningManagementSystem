namespace Domain.Entities;

public class CourseAttendance
{
    public string StudentId { get; set; } = null!;
    public User Student { get; set; } = null!;
    public string CourseId { get; set; } = null!;
    public Course Course { get; set; } = null!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
