using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Persistence.Repositories;

public class StudentRepository(AppDbContext context) : IStudentRepository
{
    public void RemoveStudent(User student) => context.Users.Remove(student);

    public async Task<IEnumerable<User>> GetAllStudentsAsync(string id, string? searchTerm)
    {
        var query = context.Users
            .Where(u => u.Id != id)
            .AsQueryable();

        if (!string.IsNullOrEmpty(searchTerm))
        {
            query = query.Where(u => u.FirstName.ToLower().Contains(searchTerm) ||
                u.LastName.ToLower().Contains(searchTerm) ||
                u.Email!.ToLower().Contains(searchTerm) ||
                u.UserName!.ToLower().Contains(searchTerm));
        }

        var students = await query
            .OrderBy(u => u.UserName)
            .AsNoTracking()
            .ToListAsync();

        return students;
    }

    public async Task<User?> GetStudentByIdAsync(string id, string currentUserId)
    {
        return await context.Users
            .Where(u => u.Id == id && u.Id != currentUserId)
            .AsNoTracking()
            .FirstOrDefaultAsync();
    }

    public async Task<User?> GetStudentByIdAsync(string id)
    {
        return await context.Users.FindAsync(id);
    }

    public async Task<CourseAttendance?> GetAttendanceByIdAsync(string studentId, string courseId)
    {
        return await context.CourseAttendances.FindAsync(studentId, courseId);
    }

    public void AddCourseAttendance(CourseAttendance attendance) => context.CourseAttendances.Add(attendance);

    public void RemoveAttendance(CourseAttendance attendance) => context.CourseAttendances.Remove(attendance);
}
