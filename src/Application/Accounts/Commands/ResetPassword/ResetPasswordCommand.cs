using Application.Accounts.DTOs;
using Application.Core;
using MediatR;

namespace Application.Accounts.Commands.ResetPassword;

public class ResetPasswordCommand(ResetPasswordDto resetPasswordDto) : IRequest<Result<Unit>>
{
    public ResetPasswordDto ResetPasswordDto { get; set; } = resetPasswordDto;
}
