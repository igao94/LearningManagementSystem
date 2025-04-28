using Application.Account.DTOs;
using Application.Core;
using Application.Interfaces;
using Domain.Interfaces;
using MediatR;

namespace Application.Account.Queries.GetCurrentUserInfo;

public class GetCurrentUserInfoHandler(IUserAccessor userAccessor,
    IUnitOfWork unitOfWork) : IRequestHandler<GetCurrentUserInfoQuery, Result<CurrentUserDto>>
{
    public async Task<Result<CurrentUserDto>> Handle(GetCurrentUserInfoQuery request,
        CancellationToken cancellationToken)
    {
        var user = await unitOfWork.AccountRepository.GetUserByIdAsync(userAccessor.GetUserId());

        if (user is null || user.Email is null || user.UserName is null)
        {
            return Result<CurrentUserDto>.Failure("User not found.", 404);
        }

        return Result<CurrentUserDto>.Success(new CurrentUserDto
        {
            Id = user.Id,
            Email = user.Email,
            Username = user.UserName
        });
    }
}
