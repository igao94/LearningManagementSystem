namespace Domain.Interfaces;

public interface IUnitOfWork
{
    IAccountRepository AccountRepository { get; }
    Task<bool> SaveChangesAsync();
}
