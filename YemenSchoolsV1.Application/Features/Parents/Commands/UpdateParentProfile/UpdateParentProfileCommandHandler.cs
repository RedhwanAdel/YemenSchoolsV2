using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Dto.Parents;
using YemenSchoolsV1.Application.Resources;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Features.Parents.Commands.UpdateParentProfile
{
    public class UpdateParentProfileCommandHandler : ResponseHandler, IRequestHandler<UpdateParentProfileCommand, Response<string>>
    {
        private readonly IParentRepository _parentRepository;
        private readonly UserManager<AppUser> _userManager;

        public UpdateParentProfileCommandHandler(
            IParentRepository parentRepository,
            UserManager<AppUser> userManager,
            IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            _parentRepository = parentRepository;
            _userManager = userManager;
        }

        public async Task<Response<string>> Handle(UpdateParentProfileCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId.ToString());
            if (user == null)
            {
                return NotFound<string>("لم يتم العثور على حساب المستخدم.");
            }

            var parent = await _parentRepository.GetParentByUserIdAsync(request.UserId);
            if (parent == null)
            {
                return NotFound<string>("لم يتم العثور على بيانات ولي الأمر.");
            }

            parent.NameAr = request.Dto.NameAr;
            parent.NameEn = request.Dto.NameEn;
            parent.PhoneNumber = request.Dto.PhoneNumber;
            parent.Address = request.Dto.Address;
            parent.Email = request.Dto.Email;
            parent.JobTitle = request.Dto.JobTitle;

            user.Email = request.Dto.Email;
            user.PhoneNumber = request.Dto.PhoneNumber;

            var userUpdateResult = await _userManager.UpdateAsync(user);
            if (!userUpdateResult.Succeeded)
            {
                var errors = string.Join("; ", userUpdateResult.Errors.Select(e => e.Description));
                return BadRequest<string>($"فشل تحديث حساب المستخدم: {errors}");
            }

            await _parentRepository.UpdateAsync(parent.Id, parent);

            return Success("تم تحديث الملف الشخصي بنجاح.");
        }
    }
}
