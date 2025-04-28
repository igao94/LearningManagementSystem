using Application.Account.Commands.Login;
using Application.Account.Commands.Register;
using Application.Account.DTOs;
using Application.Account.Queries.GetCurrentUserInfo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[AllowAnonymous]
public class AccountsController : BaseApiController
{
    [HttpPost("register")]
    public async Task<ActionResult<AccountDto>> Register(RegisterDto registerDto)
    {
        return HandleResult(await Mediator.Send(new RegisterCommand(registerDto)));
    }

    [HttpPost("login")]
    public async Task<ActionResult<AccountDto>> Login(LoginDto loginDto)
    {
        return HandleResult(await Mediator.Send(new LoginCommand(loginDto)));
    }

    [HttpGet("get-current-user")]
    public async Task<ActionResult<CurrentUserDto>> GetCurrentUser()
    {
        return HandleResult(await Mediator.Send(new GetCurrentUserInfoQuery()));
    }
}
