using Domain.Entities;

namespace Domain.Interfaces;

public interface IStudentRepository
{
    Task<IEnumerable<User>> GetAllStudentsAsync(string id, string? searchTerm);
    Task<User?> GetStudentByIdAsync(string id, string currentUserId);
}
