using MediatR;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Dto.Parents;
using YemenSchoolsV1.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace YemenSchoolsV1.Application.Features.Parents.Commands.CreateParent
{
    public class CreateParentCommandHandler : IRequestHandler<CreateParentCommand, Response<object>>
    {
        private readonly IParentRepository _parentRepository;
        private readonly UserManager<AppUser> _userManager;

        public CreateParentCommandHandler(IParentRepository parentRepository, UserManager<AppUser> userManager)
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
                return new Response<object>("يوجد ولي أمر بنفس رقم الهوية.", false)
                {
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };
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
                return new Response<object>(string.Join("; ", userResult.Errors.Select(e => e.Description)), false)
                {
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };
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
                return new Response<object>("فشل تحديث حساب المستخدم. تم إلغاء العملية.", false)
                {
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };
            }

            try
            {
                await _parentRepository.AddAsync(parent);
                return new Response<object>(new { message = "تم إنشاء ولي الأمر والمستخدم بنجاح.", parentId = parent.Id }, "تم إنشاء ولي الأمر والمستخدم بنجاح.")
                {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Succeeded = true
                };
            }
            catch
            {
                await _userManager.DeleteAsync(user);
                return new Response<object>("فشل إنشاء ولي الأمر. تم إلغاء حساب المستخدم.", false)
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError
                };
            }
        }
    }
}
