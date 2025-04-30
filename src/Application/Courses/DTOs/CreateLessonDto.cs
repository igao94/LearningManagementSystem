namespace Application.Courses.DTOs;

public class CreateLessonDto
{
    public string CourseId { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string ContentUrl { get; set; } = string.Empty;
}
