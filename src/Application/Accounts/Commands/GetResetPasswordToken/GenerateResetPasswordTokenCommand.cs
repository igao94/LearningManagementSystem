using Application.Core;
using MediatR;

namespace Application.Accounts.Commands.GetResetPasswordToken;

public class GenerateResetPasswordTokenCommand(string email) : IRequest<Result<Unit>>
{
    public string Email { get; set; } = email;
}
