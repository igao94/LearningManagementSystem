using Application.Core;
using MediatR;

namespace Application.Accounts.Commands.ResendConfirmationLink;

public class ResendConfirmationLinkCommand(string email) : IRequest<Result<Unit>>
{
    public string Email { get; set; } = email;
}
