using Application.Interfaces;
using Domain.Constants;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Infrastructure.Services.EmailService;

public class EmailLinkGenerator(IHttpContextAccessor httpContextAccessor,
    LinkGenerator linkGenerator) : IEmailLinkGenerator
{
    public string CreateVerificationLink(string tokenId)
    {
        var verificationLink = linkGenerator.GetUriByName(httpContextAccessor.HttpContext!,
            RouteNames.VerifyEmail,
            new { tokenId });

        return verificationLink ?? throw new Exception("Could not create email verification link.");
    }

    public string CreateCourseLink(string courseId)
    {
        var courseLink = linkGenerator.GetUriByName(httpContextAccessor.HttpContext!,
            RouteNames.GetCourseById,
            new { id = courseId });

        return courseLink ?? throw new Exception("Could not create course link.");
    }
}
