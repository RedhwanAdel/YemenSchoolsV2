using MediatR;
using Microsoft.Extensions.Localization;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Dto;
using YemenSchoolsV1.Application.Resources;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Features.Sections.Commands.Update
{
    public class UpdateSectionCommandHandler : ResponseHandler, IRequestHandler<UpdateSectionCommand, Response<string>>
    {
        private readonly ISectionRepository _repository;

        public UpdateSectionCommandHandler(
            ISectionRepository repository,
            IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            _repository = repository;
        }

        public async Task<Response<string>> Handle(UpdateSectionCommand request, CancellationToken cancellationToken)
        {
             if (request.Dto == null)
                return BadRequest<string>("Section data is required.");

            var section = new Section
            {
                Id = request.Id,
                Name = request.Dto.Name,
                AcademicYearId = request.Dto.AcademicYearId,
                SchoolGradeId = request.Dto.SchoolGradeId,
                Capacity = request.Dto.Capacity,
                ClassTeacherId = request.Dto.ClassTeacherId

            };

            var updated = await _repository.UpdateAsync(request.Id, section);
            if (updated == null)
                return NotFound<string>("Section not found.");

            return Success("Section updated successfully.");
        }
    }
}
