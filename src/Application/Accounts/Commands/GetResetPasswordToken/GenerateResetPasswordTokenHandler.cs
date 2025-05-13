using Application.Core;
using Application.Core.Services;
using Domain.Interfaces;
using MediatR;

namespace Application.Accounts.Commands.GetResetPasswordToken;

public class GenerateResetPasswordTokenHandler(IUnitOfWork unitOfWork,
    IEmailService emailService) : IRequestHandler<GenerateResetPasswordTokenCommand, Result<Unit>>
{
    public async Task<Result<Unit>>
        Handle(GenerateResetPasswordTokenCommand request, CancellationToken cancellationToken)
    {
        var user = await unitOfWork.AccountRepository.GetUserByEmailAsync(request.Email);

        if (user is null || user.Email is null)
        {
            return Result<Unit>.Failure("User not found.", 404);
        }

        var resetToken = await unitOfWork.AccountRepository.GenerateResetPasswordTokenAsync(user);

        if (string.IsNullOrEmpty(resetToken))
        {
            return Result<Unit>.Failure("Failed to generate reset token.", 400);
        }

        var result = await emailService.SendResetPasswordTokenAsync(user.Email, resetToken);

        return result
            ? Result<Unit>.Success(Unit.Value)
            : Result<Unit>.Failure("Failed to send reset token.", 400);
    }
}
