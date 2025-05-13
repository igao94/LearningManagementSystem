namespace Application.Interfaces;

public interface IEmailSender
{
    Task<bool> SendConfirmationLinkAsync(string userEmail, string verificationLink);
    Task<bool> SendCourseLinkAsync(string userEmail, string courseName, string courseLink);
    Task<bool> SendResetPasswordTokenAsync(string userEmail, string resetToken);
    Task<bool> SendCompletedCourseNotificationAsync(string userEmail, string courseName, string courseLink);
}
