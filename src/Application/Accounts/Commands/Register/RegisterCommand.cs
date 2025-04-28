using Application.Accounts.DTOs;
using Application.Core;
using MediatR;

namespace Application.Accounts.Commands.Register;

public class RegisterCommand(RegisterDto registerDto) : IRequest<Result<AccountDto>>
{
    public RegisterDto RegisterDto { get; set; } = registerDto;
}
