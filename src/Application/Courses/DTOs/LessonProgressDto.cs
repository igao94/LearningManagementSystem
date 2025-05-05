namespace Application.Courses.DTOs;

public class LessonProgressDto
{
    public int CompletedLessons { get; set; }
    public int TotalLessons { get; set; }
    public double ProgressPercentage => TotalLessons == 0 ? 0 : (double)CompletedLessons / TotalLessons * 100;
}
