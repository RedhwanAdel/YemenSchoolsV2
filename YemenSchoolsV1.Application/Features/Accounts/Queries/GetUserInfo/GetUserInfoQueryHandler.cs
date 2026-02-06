using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Dto;
using YemenSchoolsV1.Application.Extensions;
using YemenSchoolsV1.Application.Resources;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Features.Accounts.Queries.GetUserInfo
{
    public class GetUserInfoQueryHandler : ResponseHandler, IRequestHandler<GetUserInfoQuery, Response<UserInfoDto>>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITeacherRepository _teacherRepository;

        public GetUserInfoQueryHandler(
            UserManager<AppUser> userManager,
            ITeacherRepository teacherRepository,
            IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            _userManager = userManager;
            _teacherRepository = teacherRepository;
        }

        public async Task<Response<UserInfoDto>> Handle(GetUserInfoQuery request, CancellationToken cancellationToken)
        {
            if (request.User.Identity?.IsAuthenticated == false)
            {
                return Unauthorized<UserInfoDto>();
            }

            var user = await _userManager.GetUserByEmail(request.User);
            if (user == null)
            {
                return Unauthorized<UserInfoDto>();
            }

            var userInfo = new UserInfoDto
            {
                Id = user.Id,
                Name = user.Name,
                ImageUrl = user.ImageUrl,
                Email = user.Email,
                EntityId = user.EntityId,
                UserType = user.UserType,
                SchoolId = user.SchoolId
            };

            if (user.UserType == "Teacher")
            {
                var teacher = await _teacherRepository.GetByIdAsync(user.EntityId);
                if (teacher != null)
                {
                    userInfo.SchoolId = teacher.SchoolId;
                }
            }

            return Success(userInfo);
        }
    }
}
