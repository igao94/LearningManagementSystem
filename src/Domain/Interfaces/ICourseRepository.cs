using Domain.Entities;

namespace Domain.Interfaces;

public interface ICourseRepository
{
    Task<IEnumerable<Course>> GetAllCoursesAsync(string? searchTerm, string? filter, string currentUserId);
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
    Task<Course?> GetCourseWithAttendeesAndLessonsAndProgressAndCertificateByIdAsync(string id);
    Task<bool> AreLessonsCompletedByStudentAsync(string courseId, string studentId, int lessonCount);
    Task<Certificate?> GetCertificateByIdAsync(string studentId, string courseId);
    void AddCertificate(Certificate certificate);
    void RemoveCertificates(IEnumerable<Certificate> certificates);
    Task<int> GetLessonsCountAsync(string courseId);
    Task<int> GetCompletedLessonsCountForStudentAsync(string studentId, string courseId);
}
