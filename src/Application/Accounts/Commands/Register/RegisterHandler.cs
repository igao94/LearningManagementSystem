﻿using Application.Accounts.DTOs;
using Application.Core;
using Application.Interfaces;
using Domain.Constants;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Accounts.Commands.Register;

public class RegisterHandler(IUnitOfWork unitOfWork,
    ITokenService tokenService,
    IEmailVerificationLinkFactory emailVerificationLinkFactory,
    IEmailSender emailSender) : IRequestHandler<RegisterCommand, Result<AccountDto>>
{
    public async Task<Result<AccountDto>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        if (await unitOfWork.AccountRepository.UsernameExistsAsync(request.RegisterDto.Username))
        {
            return Result<AccountDto>.Failure("Username is already taken.", 400);
        }

        if (await unitOfWork.AccountRepository.EmailExistsAsync(request.RegisterDto.Email))
        {
            return Result<AccountDto>.Failure("Email is already taken.", 400);
        }

        var user = new User
        {
            Email = request.RegisterDto.Email,
            UserName = request.RegisterDto.Username,
            FirstName = request.RegisterDto.FirstName,
            LastName = request.RegisterDto.LastName
        };

        var result = await unitOfWork.AccountRepository.RegisterUserAsync(user, request.RegisterDto.Password);

        if (!result.Succeeded)
        {
            return Result<AccountDto>.Failure("Failed to create user.", 400);
        }

        var verificationTokenId = CreateAndAttachVerificationToken(user);

        await SendConfirmationLinkAsync(user.Email, verificationTokenId);

        var roleResult = await unitOfWork.AccountRepository.AddToRoleAsync(user, UserRoles.Student);

        if (!roleResult.Succeeded)
        {
            return Result<AccountDto>.Failure("Failed to add to role.", 400);
        }

        var token = await tokenService.GetTokenAsync(user);

        return Result<AccountDto>.Success(new AccountDto
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            Username = user.Email,
            Token = token
        });
    }

    private static string CreateAndAttachVerificationToken(User user)
    {
        var verificationToken = new EmailVerificationToken
        {
            StudentId = user.Id,
            CreatedOn = DateTime.UtcNow,
            ExpiresAt = DateTime.UtcNow.AddHours(1)
        };

        user.EmailVerificationTokens.Add(verificationToken);

        return verificationToken.Id;
    }

    private async Task SendConfirmationLinkAsync(string userEmail, string tokenId)
    {
        var verificationLink = emailVerificationLinkFactory.CreateVerificationLink(tokenId);

        await emailSender.SendConfirmationLinkAsync(userEmail, verificationLink);
    }
}
