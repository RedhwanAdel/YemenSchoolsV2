using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Dto.Parents;
using YemenSchoolsV1.Application.Resources;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Features.Parents.Commands.CreateParent
{
    public class CreateParentCommandHandler : ResponseHandler, IRequestHandler<CreateParentCommand, Response<object>>
    {
        private readonly IParentRepository _parentRepository;
        private readonly UserManager<AppUser> _userManager;

        public CreateParentCommandHandler(
            IParentRepository parentRepository,
            UserManager<AppUser> userManager,
            IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            _parentRepository = parentRepository;
            _userManager = userManager;
        }

        public async Task<Response<object>> Handle(CreateParentCommand request, CancellationToken cancellationToken)
        {
            var dto = request.Dto;
            string defaultPassword = "Pa$$w0rd";

            if (await _parentRepository.ParentExistsByNationalIdAsync(dto.NationalId))
            {
                return BadRequest<object>("يوجد ولي أمر بنفس رقم الهوية.");
            }

            var user = new AppUser
            {
                UserName = dto.NationalId,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                Name = dto.NameAr,
                UserType = "Parent"
            };

            var userResult = await _userManager.CreateAsync(user, defaultPassword);
            if (!userResult.Succeeded)
            {
                return BadRequest<object>(string.Join("; ", userResult.Errors.Select(e => e.Description)));
            }

            var parent = new Parent
            {
                Id = Guid.NewGuid(),
                NameAr = dto.NameAr,
                NameEn = dto.NameEn,
                PhoneNumber = dto.PhoneNumber,
                Address = dto.Address,
                NationalId = dto.NationalId,
                Email = dto.Email,
                Gender = dto.Gender,
                JobTitle = dto.JobTitle,
                DateOfBirth = dto.DateOfBirth,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UserId = user.Id,
            };

            user.EntityId = parent.Id;
            var updateUserResult = await _userManager.UpdateAsync(user);
            if (!updateUserResult.Succeeded)
            {
                await _userManager.DeleteAsync(user);
                return BadRequest<object>("فشل تحديث حساب المستخدم. تم إلغاء العملية.");
            }

            try
            {
                await _parentRepository.AddAsync(parent);
                return Success((object)new { message = "تم إنشاء ولي الأمر والمستخدم بنجاح.", parentId = parent.Id }, "تم إنشاء ولي الأمر والمستخدم بنجاح.");
            }
            catch
            {
                await _userManager.DeleteAsync(user);
                return UnprocessableEntity<object>("فشل إنشاء ولي الأمر. تم إلغاء حساب المستخدم.");
            }
        }
    }
}
