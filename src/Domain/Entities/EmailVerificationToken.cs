namespace Domain.Entities;

public class EmailVerificationToken
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string StudentId { get; set; } = null!;
    public User Student { get; set; } = null!;
    public DateTime CreatedOn { get; set; }
    public DateTime ExpiresAt { get; set; }
}
