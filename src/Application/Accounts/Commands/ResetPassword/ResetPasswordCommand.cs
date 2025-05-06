using Application.Accounts.DTOs;
using Application.Core;
using MediatR;

namespace Application.Accounts.Commands.ResetPassword;

public class ResetPasswordCommand(string email, string newPassword) : IRequest<Result<ResetPasswordDto>>
{
    public string Email { get; set; } = email;
    public string NewPassword { get; set; } = newPassword;
}
