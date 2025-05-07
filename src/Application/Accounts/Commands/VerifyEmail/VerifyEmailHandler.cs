using Application.Core;
using Domain.Interfaces;
using MediatR;

namespace Application.Accounts.Commands.VerifyEmail;

public class VerifyEmailHandler(IUnitOfWork unitOfWork) : IRequestHandler<VerifyEmailCommand, Result<Unit>>
{
    public async Task<Result<Unit>> Handle(VerifyEmailCommand request, CancellationToken cancellationToken)
    {
        var token = await unitOfWork.AccountRepository.GetTokenWithStudentAsync(request.TokenId);

        if (token is null || token.ExpiresAt < DateTime.UtcNow || token.Student.EmailConfirmed)
        {
            return Result<Unit>.Failure("Token not valid.", 400);
        }

        token.Student.EmailConfirmed = true;

        unitOfWork.AccountRepository.RemoveToken(token);

        var result = await unitOfWork.SaveChangesAsync();

        return result
            ? Result<Unit>.Success(Unit.Value)
            : Result<Unit>.Failure("Failed to verify email.", 400);
    }
}
