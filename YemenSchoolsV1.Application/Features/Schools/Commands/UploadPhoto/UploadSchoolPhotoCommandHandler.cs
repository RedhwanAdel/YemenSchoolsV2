using MediatR;
using Microsoft.Extensions.Localization;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Resources;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Features.Schools.Commands.UploadPhoto
{
    public class UploadSchoolPhotoCommandHandler : ResponseHandler, IRequestHandler<UploadSchoolPhotoCommand, Response<SchoolPhoto>>
    {
        private readonly ISchoolRepository _repository;

        public UploadSchoolPhotoCommandHandler(ISchoolRepository repository, IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            _repository = repository;
        }

        public async Task<Response<SchoolPhoto>> Handle(UploadSchoolPhotoCommand request, CancellationToken cancellationToken)
        {
            var file = request.File;
            if (file == null || file.Length == 0)
                return BadRequest<SchoolPhoto>("No file uploaded");

            string folder = Path.Combine("wwwroot", "uploads", "schools");
            Directory.CreateDirectory(folder);

            string fileName = $"{Guid.NewGuid()}_{file.FileName}";
            string filePath = Path.Combine(folder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            string fileUrl = $"https://localhost:5001/uploads/schools/{fileName}"; 

            var schoolPhoto = new SchoolPhoto
            {
                SchoolId = request.SchoolId,
                PhotoUrl = fileUrl
            };

            await _repository.AddSchoolPhotoAsync(schoolPhoto);

            return Success(schoolPhoto);
        }
    }
}
