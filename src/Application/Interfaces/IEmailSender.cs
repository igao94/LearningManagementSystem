namespace Application.Interfaces;

public interface IEmailSender
{
    Task SendConfirmationLinkAsync(string userEmail, string verificationLink);
    Task SendCourseLinkAsync(string userEmail, string courseName, string courseLink);
    Task SendResetPasswordTokenAsync(string userEmail, string resetToken);
}
