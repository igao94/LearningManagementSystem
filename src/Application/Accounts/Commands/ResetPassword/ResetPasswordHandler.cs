using Application.Core;
using Domain.Interfaces;
using MediatR;

namespace Application.Accounts.Commands.ResetPassword;

public class ResetPasswordHandler(IUnitOfWork unitOfWork) : IRequestHandler<ResetPasswordCommand, Result<Unit>>
{
    public async Task<Result<Unit>> Handle(ResetPasswordCommand request,
        CancellationToken cancellationToken)
    {
        var user = await unitOfWork.AccountRepository.GetUserByEmailAsync(request.ResetPasswordDto.Email);

        if (user is null || user.Email is null)
        {
            return Result<Unit>.Failure("User not found.", 404);
        }

        var isOldPassword = await unitOfWork.AccountRepository
            .CheckPasswordAsync(user, request.ResetPasswordDto.NewPassword);

        if (isOldPassword)
        {
            return Result<Unit>.Failure("Old password can't be same as new one.", 400);
        }

        var result = await unitOfWork.AccountRepository
            .ResetPasswordAsync(user, request.ResetPasswordDto.ResetToken, request.ResetPasswordDto.NewPassword);

        return result.Succeeded
            ? Result<Unit>.Success(Unit.Value)
            : Result<Unit>.Failure("Failed to reset password.", 400);
    }
}
