using Domain.Entities;

namespace Domain.Interfaces;

public interface ICourseRepository
{
    Task<IEnumerable<Course>> GetAllCoursesAsync(string? searchTerm, string? sort);
    Task<Course?> GetCourseByIdAsync(string id);
    Task<Course?> GetCourseWithLessonsByIdAsync(string id);
    void AddCourse(Course course);
    void RemoveCourse(Course course);
    Task<bool> CourseExistsAsync(string title);
    Task<Lesson?> GetLessonByIdAsync(string id);
    void RemoveLesson(Lesson lesson);
}
