using Application.Account.DTOs;
using Application.Core;
using MediatR;

namespace Application.Account.Queries.GetCurrentUserInfo;

public class GetCurrentUserInfoQuery : IRequest<Result<CurrentUserDto>>
{

}
