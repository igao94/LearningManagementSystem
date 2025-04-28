using Application.Account.DTOs;
using Application.Account.Login;
using Application.Account.Register;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class AccountsController : BaseApiController
{
    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<ActionResult<AccountDto>> Register(RegisterDto registerDto)
    {
        return HandleResult(await Mediator.Send(new RegisterCommand(registerDto)));
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<ActionResult<AccountDto>> Login(LoginDto loginDto)
    {
        return HandleResult(await Mediator.Send(new LoginCommand(loginDto)));
    }
}
