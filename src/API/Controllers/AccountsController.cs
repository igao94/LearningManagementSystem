using Application.Accounts.Commands.Login;
using Application.Accounts.Commands.Register;
using Application.Accounts.Commands.ResetPassword;
using Application.Accounts.Commands.VerifyEmail;
using Application.Accounts.DTOs;
using Application.Accounts.Queries.GetCurrentUserInfo;
using Domain.Constants;
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

    [AllowAnonymous]
    [HttpGet("get-current-user")]
    public async Task<ActionResult<CurrentUserDto>> GetCurrentUser()
    {
        return HandleResult(await Mediator.Send(new GetCurrentUserInfoQuery()));
    }

    [AllowAnonymous]
    [HttpPost("reset-password")]
    public async Task<ActionResult<ResetPasswordDto>> ResetPassword(ResetPasswordCommand command)
    {
        return HandleResult(await Mediator.Send(command));
    }

    [AllowAnonymous]
    [HttpGet("verify-email/{tokenId}", Name = RouteNames.VerifyEmail)]
    public async Task<ActionResult> VerifyEmail(string tokenId)
    {
        return HandleResult(await Mediator.Send(new VerifyEmailCommand(tokenId)));
    }
}
