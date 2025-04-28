using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Persistence.Repositories;

public class StudentRepository(AppDbContext context) : IStudentRepository
{
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
}
