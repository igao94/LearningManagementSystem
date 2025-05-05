using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Persistence.Repositories;

public class CourseRepository(AppDbContext context) : ICourseRepository
{
    public async Task<IEnumerable<Course>> GetAllCoursesAsync(string? searchTerm, string? sort)
    {
        var query = context.Courses
            .Select(c => new Course
            {
                Id = c.Id,
                Title = c.Title,
                Description = c.Description,
                InstructorName = c.InstructorName,
                CreatedAt = c.CreatedAt,
                Attendees = c.Attendees.Select(ca => new CourseAttendance
                {
                    StudentId = ca.StudentId,
                })
                .ToList()
            })
            .AsQueryable();

        if (!string.IsNullOrEmpty(searchTerm))
        {
            query = query.Where(c => c.Title.ToLower().Contains(searchTerm) ||
                c.InstructorName.ToLower().Contains(searchTerm));
        }

        query = sort switch
        {
            "dateAsc" => query.OrderBy(c => c.CreatedAt),
            "dateDesc" => query.OrderByDescending(c => c.CreatedAt),
            _ => query.OrderBy(c => c.Title)
        };

        var courses = await query.AsNoTracking().ToListAsync();

        return courses;
    }

    public async Task<Course?> GetCourseByIdAsync(string id) => await context.Courses.FindAsync(id);

    public async Task<Course?> GetCourseWithAttendeesAndLessonsAndProgressAndCertificateByIdAsync(string id)
    {
        return await context.Courses
            .Include(c => c.Attendees)
            .Include(c => c.Certificates)
            .Include(c => c.Lessons)
                .ThenInclude(l => l.LessonProgresses)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<Course?> GetCourseWithLessonsByIdAsync(string id)
    {
        return await context.Courses
            .Select(c => new Course
            {
                Id = c.Id,
                Title = c.Title,
                Description = c.Description,
                InstructorName = c.InstructorName,
                CreatedAt = c.CreatedAt,
                Attendees = c.Attendees.Select(ca => new CourseAttendance
                {
                    StudentId = ca.StudentId,
                }).ToList(),
                Lessons = c.Lessons.Select(l => new Lesson
                {
                    Id = l.Id,
                    Title = l.Title,
                    ContentUrl = l.ContentUrl
                }).ToList()
            })
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public void AddCourse(Course course) => context.Courses.Add(course);

    public async Task<bool> CourseExistsAsync(string title)
    {
        return await context.Courses.AnyAsync(c => c.Title.ToLower() == title.ToLower());
    }

    public void RemoveCourse(Course course) => context.Courses.Remove(course);

    public async Task<Lesson?> GetLessonByIdAsync(string id) => await context.Lessons.FindAsync(id);

    public async Task<Lesson?> GetLessonWithProgressByIdAsync(string id)
    {
        return await context.Lessons
            .Include(l => l.LessonProgresses)
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public void RemoveLesson(Lesson lesson) => context.Lessons.Remove(lesson);

    public async Task<IEnumerable<User>> GetCourseAttendees(string id)
    {
        return await context.CourseAttendances
            .Where(ca => ca.CourseId == id)
            .Select(ca => new User
            {
                Id = ca.StudentId,
                Email = ca.Student.Email,
                UserName = ca.Student.UserName,
                FirstName = ca.Student.FirstName,
                LastName = ca.Student.LastName
            })
            .ToListAsync();
    }

    public async Task<LessonProgress?> GetLessonProgressAsync(string studentId, string lessonId)
    {
        return await context.LessonProgresses.FindAsync(studentId, lessonId);
    }

    public void AddLessonProgress(LessonProgress lessonProgress)
    {
        context.LessonProgresses.Add(lessonProgress);
    }

    public void RemoveLessonProgresses(IEnumerable<LessonProgress> lessonProgresses)
    {
        context.LessonProgresses.RemoveRange(lessonProgresses);
    }

    public async Task<int> GetLessonsCountAsync(string courseId)
    {
        return await context.Lessons.CountAsync(ca => ca.CourseId == courseId);
    }

    public async Task<int> GetCompletedLessonsCountForStudentAsync(string studentId, string courseId)
    {
        return await context.LessonProgresses
            .CountAsync(lp => lp.StudentId == studentId && lp.Lesson.CourseId == courseId);
    }

    public async Task<bool> AreLessonsCompletedByStudentAsync(string courseId, string studentId, int lessonCount)
    {
        var lessonProgress = await context.LessonProgresses
            .Where(lp => lp.Lesson.CourseId == courseId && lp.StudentId == studentId)
            .ToListAsync();

        if (!lessonProgress.Any())
        {
            return false;
        }

        if (lessonProgress.Count != lessonCount)
        {
            return false;
        }

        return true;
    }

    public void AddCertificate(Certificate certificate) => context.Certificates.Add(certificate);


    public async Task<Certificate?> GetCertificateByIdAsync(string studentId, string courseId)
    {
        return await context.Certificates.FindAsync(studentId, courseId);
    }

    public void RemoveCertificates(IEnumerable<Certificate> certificates)
    {
        context.Certificates.RemoveRange(certificates);
    }
}
