using Application.Core;
using MediatR;

namespace Application.Accounts.Commands.VerifyEmail;

public class VerifyEmailCommand(string tokenId) : IRequest<Result<Unit>>
{
    public string TokenId { get; set; } = tokenId;
}
