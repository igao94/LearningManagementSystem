using Application.Account.DTOs;
using Application.Core;
using MediatR;

namespace Application.Account.Register;

public class RegisterCommand(RegisterDto registerDto) : IRequest<Result<AccountDto>>
{
    public RegisterDto RegisterDto { get; set; } = registerDto;
}
