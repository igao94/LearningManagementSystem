using Application.Core;
using Application.Interfaces;
using Domain.Interfaces;
using MediatR;

namespace Application.Accounts.Commands.GetResetPasswordToken;

public class GetResetPasswordTokenHandler(IUnitOfWork unitOfWork,
    IEmailSender emailSender) : IRequestHandler<GetResetPasswordTokenQuery, Result<Unit>>
{
    public async Task<Result<Unit>>
        Handle(GetResetPasswordTokenQuery request, CancellationToken cancellationToken)
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

        await emailSender.SendResetPasswordTokenAsync(user.Email, resetToken);

        return Result<Unit>.Success(Unit.Value);
    }
}
