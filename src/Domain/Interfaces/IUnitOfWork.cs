namespace Domain.Interfaces;

public interface IUnitOfWork
{
    IAccountRepository AccountRepository { get; }
    IStudentRepository StudentRepository { get; }
    Task<bool> SaveChangesAsync();
}
