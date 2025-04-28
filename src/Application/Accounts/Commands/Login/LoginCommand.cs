using Application.Accounts.DTOs;
using Application.Core;
using MediatR;

namespace Application.Accounts.Commands.Login;

public class LoginCommand(LoginDto loginDto) : IRequest<Result<AccountDto>>
{
    public LoginDto LoginDto { get; set; } = loginDto;
}
