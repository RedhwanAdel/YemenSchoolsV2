using MediatR;
using System.Security.Claims;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Dto;

namespace YemenSchoolsV1.Application.Features.Accounts.Queries.GetUserInfo
{
    public class GetUserInfoQuery : IRequest<Response<UserInfoDto>>
    {
        public ClaimsPrincipal User { get; set; }

        public GetUserInfoQuery(ClaimsPrincipal user)
        {
            User = user;
        }
    }
}
