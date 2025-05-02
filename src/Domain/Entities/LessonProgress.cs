namespace Domain.Entities;

public class LessonProgress
{
    public string StudentId { get; set; } = null!;
    public User Student { get; set; } = null!;
    public string LessonId { get; set; } = null!;
    public Lesson Lesson { get; set; } = null!;
    public bool IsCompleted { get; set; }
    public DateTime CompletedAt { get; set; }
}
