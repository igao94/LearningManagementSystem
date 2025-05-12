using Application.Core;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Accounts.Commands.ResendConfirmationLink;

public class ResendConfirmationLinkHandler(IUnitOfWork unitOfWork,
    IEmailLinkGenerator emailVerificationLinkFactory,
    IEmailSender emailSender) : IRequestHandler<ResendConfirmationLinkCommand, Result<Unit>>
{
    public async Task<Result<Unit>> Handle(ResendConfirmationLinkCommand request,
        CancellationToken cancellationToken)
    {
        var student = await unitOfWork.AccountRepository.GetUserByEmailWithTokensAsync(request.Email);

        if (student is null || student.Email is null)
        {
            return Result<Unit>.Failure("Student not found.", 404);
        }

        if (student.EmailConfirmed)
        {
            return Result<Unit>.Failure("You have already verified email.", 400);
        }

        var lastTokenRequestTime = student.EmailVerificationTokens
            .OrderByDescending(et => et.CreatedOn)
            .FirstOrDefault()?.CreatedOn;

        if (lastTokenRequestTime.HasValue && (DateTime.UtcNow - lastTokenRequestTime.Value).TotalMinutes < 5)
        {
            return Result<Unit>.Failure("You need to wait 5 minutes before requesting new token.", 400);
        }

        student.EmailVerificationTokens.Clear();

        var token = new EmailVerificationToken
        {
            StudentId = student.Id,
            CreatedOn = DateTime.UtcNow,
            ExpiresAt = DateTime.UtcNow.AddHours(1)
        };

        student.EmailVerificationTokens.Add(token);

        var confirmationLink = emailVerificationLinkFactory.CreateVerificationLink(token.Id);

        var emailResult = await emailSender.SendConfirmationLinkAsync(student.Email, confirmationLink);

        if (!emailResult)
        {
            return Result<Unit>.Failure("Failed to send email.", 400);
        }

        var result = await unitOfWork.SaveChangesAsync();

        return result
            ? Result<Unit>.Success(Unit.Value)
            : Result<Unit>.Failure("Failed to save token in database.", 400);
    }
}
