namespace Application.Interfaces;

public interface IEmailLinkGenerator
{
    string CreateVerificationLink(string tokenId);
    string CreateCourseLink(string courseId);
}
