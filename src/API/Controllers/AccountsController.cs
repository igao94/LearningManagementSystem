using Application.Accounts.Commands.GetResetPasswordToken;
using Application.Accounts.Commands.Login;
using Application.Accounts.Commands.Register;
using Application.Accounts.Commands.ResendConfirmationLink;
using Application.Accounts.Commands.ResetPassword;
using Application.Accounts.Commands.VerifyEmail;
using Application.Accounts.DTOs;
using Application.Accounts.Queries.GetCurrentUserInfo;
using Domain.Constants;
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

    [HttpGet("get-reset-password-token/{email}")]
    public async Task<ActionResult> GetResetPasswordToken(string email)
    {
        return HandleResult(await Mediator.Send(new GetResetPasswordTokenQuery(email)));
    }

    [HttpPost("reset-password")]
    public async Task<ActionResult<ResetPasswordDto>> ResetPassword(ResetPasswordDto resetPasswordDto)
    {
        return HandleResult(await Mediator.Send(new ResetPasswordCommand(resetPasswordDto)));
    }

    [HttpGet("verify-email/{tokenId}", Name = RouteNames.VerifyEmail)]
    public async Task<ActionResult> VerifyEmail(string tokenId)
    {
        return HandleResult(await Mediator.Send(new VerifyEmailCommand(tokenId)));
    }

    [HttpPost("resend-confirmation-link/{email}")]
    public async Task<ActionResult> ResendConfirmationLink(string email)
    {
        return HandleResult(await Mediator.Send(new ResendConfirmationLinkCommand(email)));
    }
}
