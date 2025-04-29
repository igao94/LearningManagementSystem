using Domain.Entities;

namespace Domain.Interfaces;

public interface ICourseRepository
{
    Task<IEnumerable<Course>> GetAllCoursesAsync(string? searchTerm, string? sort);
}
