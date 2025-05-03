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
    Task<IEnumerable<User>> GetCourseAttendees(string id);
    Task<LessonProgress?> GetLessonProgressAsync(string studentId, string lessonId);
    void AddLessonProgress(LessonProgress lessonProgress);
    void RemoveLessonProgresses(IEnumerable<LessonProgress> lessonProgresses);
    Task<Lesson?> GetLessonWithProgressByIdAsync(string id);
    Task<Course?> GetCourseWithAttendeesAndLessonsAndProgressByIdAsync(string id);
}
