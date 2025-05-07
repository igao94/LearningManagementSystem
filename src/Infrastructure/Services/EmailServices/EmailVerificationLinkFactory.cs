using Application.Interfaces;
using Domain.Constants;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Infrastructure.Services.EmailServices;

public class EmailVerificationLinkFactory(IHttpContextAccessor httpContextAccessor,
    LinkGenerator linkGenerator) : IEmailVerificationLinkFactory
{
    public string Create(EmailVerificationToken token)
    {
        var verificationLink = linkGenerator.GetUriByName(httpContextAccessor.HttpContext!,
            RouteNames.VerifyEmail,
            new { tokenId = token.Id });

        return verificationLink ?? throw new Exception("Could not create email verification link.");
    }
}
