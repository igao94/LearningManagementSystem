using Application.Core;
using MediatR;

namespace Application.Accounts.Commands.GetResetPasswordToken;

public class GetResetPasswordTokenQuery(string email) : IRequest<Result<Unit>>
{
    public string Email { get; set; } = email;
}
