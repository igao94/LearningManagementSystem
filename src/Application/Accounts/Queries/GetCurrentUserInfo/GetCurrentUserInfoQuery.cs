using Application.Accounts.DTOs;
using Application.Core;
using MediatR;

namespace Application.Accounts.Queries.GetCurrentUserInfo;

public class GetCurrentUserInfoQuery : IRequest<Result<CurrentUserDto>>
{

}
