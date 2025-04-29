namespace Domain.Entities;

public class Lesson
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Title { get; set; } = string.Empty;
    public string ContentUrl { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public Course Course { get; set; } = null!;
    public string CourseId { get; set; } = null!;
}
