namespace Domain.Interfaces;

public interface IUnitOfWork
{
    IAccountRepository AccountRepository { get; }
    IStudentRepository StudentRepository { get; }
    ICourseRepository CourseRepository { get; }
    IEmailTokenRepository EmailTokenRepository { get; }
    Task<bool> SaveChangesAsync();
}
