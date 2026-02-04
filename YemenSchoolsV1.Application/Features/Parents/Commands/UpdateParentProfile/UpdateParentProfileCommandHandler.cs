using MediatR;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Dto.Parents;
using YemenSchoolsV1.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace YemenSchoolsV1.Application.Features.Parents.Commands.UpdateParentProfile
{
    public class UpdateParentProfileCommandHandler : IRequestHandler<UpdateParentProfileCommand, Response<string>>
    {
        private readonly IParentRepository _parentRepository;
        private readonly UserManager<AppUser> _userManager;

        public UpdateParentProfileCommandHandler(IParentRepository parentRepository, UserManager<AppUser> userManager)
        {
            _parentRepository = parentRepository;
            _userManager = userManager;
        }

        public async Task<Response<string>> Handle(UpdateParentProfileCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId.ToString());
            if (user == null)
            {
                return new Response<string>("لم يتم العثور على حساب المستخدم.", false)
                {
                    StatusCode = System.Net.HttpStatusCode.NotFound
                };
            }

            var parent = await _parentRepository.GetParentByUserIdAsync(request.UserId);
            if (parent == null)
            {
                return new Response<string>("لم يتم العثور على بيانات ولي الأمر.", false)
                {
                    StatusCode = System.Net.HttpStatusCode.NotFound
                };
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
                return new Response<string>($"فشل تحديث حساب المستخدم: {errors}", false)
                {
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };
            }

            await _parentRepository.UpdateAsync(parent.Id, parent);

            return new Response<string>("تم تحديث الملف الشخصي بنجاح.", true)
            {
                StatusCode = System.Net.HttpStatusCode.OK
            };
        }
    }
}
