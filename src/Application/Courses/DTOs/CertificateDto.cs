namespace Application.Courses.DTOs;

public class CertificateDto
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Course { get; set; } = string.Empty;
    public string CertificateUrl { get; set; } = string.Empty;
    public DateTime IssuedAt { get; set; }
}
