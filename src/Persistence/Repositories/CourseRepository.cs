using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Persistence.Repositories;

public class CourseRepository(AppDbContext context) : ICourseRepository
{
    public async Task<IEnumerable<Course>> GetAllCoursesAsync(string? searchTerm, string? sort)
    {
        var query = context.Courses.AsQueryable();

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
                Lessons = c.Lessons.Select(l => new Lesson
                {
                    Id = l.Id,
                    Title = l.Title,
                    ContentUrl = l.ContentUrl
                })
                .ToList()
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
}
