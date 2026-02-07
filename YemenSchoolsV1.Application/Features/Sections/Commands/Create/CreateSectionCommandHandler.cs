using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Dto;
using YemenSchoolsV1.Application.Resources;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Features.Sections.Commands.Create
{
    public class CreateSectionCommandHandler : ResponseHandler, IRequestHandler<CreateSectionCommand, Response<string>>
    {
        private readonly ISectionRepository _repository;
        private readonly IMapper _mapper;

        public CreateSectionCommandHandler(
            ISectionRepository repository,
            IMapper mapper,
            IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response<string>> Handle(CreateSectionCommand request, CancellationToken cancellationToken)
        {
            if (request.Dto == null)
                return BadRequest<string>("Section data is required.");

            var section = _mapper.Map<Section>(request.Dto);
            // Manual mapping for safety if mapper fails or is not configured
            if (section == null)
            {
                section = new Section{
                     Name = request.Dto.Name,
                     AcademicYearId = request.Dto.AcademicYearId,
                     SchoolGradeId = request.Dto.SchoolGradeId,
                     Capacity = request.Dto.Capacity,
                     ClassTeacherId = request.Dto.ClassTeacherId
                };
            }

            var created = await _repository.AddAsync(section);
            if (created == null)
                return UnprocessableEntity<string>("Failed to create section.");

            return Created("Section created successfully.");
        }
    }
}
