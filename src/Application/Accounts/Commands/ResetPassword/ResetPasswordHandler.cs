using Application.Accounts.DTOs;
using Application.Core;
using Application.Interfaces;
using Domain.Interfaces;
using MediatR;

namespace Application.Accounts.Commands.ResetPassword;

public class ResetPasswordHandler(IUnitOfWork unitOfWork,
    ITokenService tokenService) : IRequestHandler<ResetPasswordCommand, Result<ResetPasswordDto>>
{
    public async Task<Result<ResetPasswordDto>> Handle(ResetPasswordCommand request,
        CancellationToken cancellationToken)
    {
        var user = await unitOfWork.AccountRepository.GetUserByEmailAsync(request.Email);

        if (user is null || user.Email is null)
        {
            return Result<ResetPasswordDto>.Failure("User not found.", 404);
        }

        var isOldPassword = await unitOfWork.AccountRepository.CheckPasswordAsync(user, request.NewPassword);

        if (isOldPassword)
        {
            return Result<ResetPasswordDto>.Failure("Old password can't be same as new one.", 404);
        }

        var resetToken = await unitOfWork.AccountRepository.GenerateResetPasswordTokenAsync(user);

        var bearerToken = await tokenService.GetTokenAsync(user);

        var result = await unitOfWork.AccountRepository
            .ResetPasswordAsync(user, resetToken, request.NewPassword);

        return result.Succeeded
            ? Result<ResetPasswordDto>.Success(new ResetPasswordDto
            {
                Email = user.Email,
                NewPassword = request.NewPassword,
                Token = bearerToken
            })
            : Result<ResetPasswordDto>.Failure("Failed to reset password.", 400);
    }
}
