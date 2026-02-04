using MediatR;
using YemenSchoolsV1.Application.Bases;
using Microsoft.AspNetCore.Http;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Features.Schools.Commands.UploadPhoto
{
    public class UploadSchoolPhotoCommand : IRequest<Response<SchoolPhoto>>
    {
        public IFormFile File { get; set; }
        public Guid SchoolId { get; set; }
        public UploadSchoolPhotoCommand(IFormFile file, Guid schoolId)
        {
            File = file;
            SchoolId = schoolId;
        }
    }
}
