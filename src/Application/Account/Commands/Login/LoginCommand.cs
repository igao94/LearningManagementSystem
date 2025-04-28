using Application.Account.DTOs;
using Application.Core;
using MediatR;

namespace Application.Account.Commands.Login;

public class LoginCommand(LoginDto loginDto) : IRequest<Result<AccountDto>>
{
    public LoginDto LoginDto { get; set; } = loginDto;
}
