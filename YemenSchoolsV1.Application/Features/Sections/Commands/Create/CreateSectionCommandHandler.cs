using MediatR;
using YemenSchoolsV1.Application.Bases;
using AutoMapper;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Dto;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Features.Sections.Commands.Create
{
    public class CreateSectionCommandHandler : IRequestHandler<CreateSectionCommand, Response<string>>
    {
        private readonly ISectionRepository _repository;
        private readonly IMapper _mapper;

        public CreateSectionCommandHandler(ISectionRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response<string>> Handle(CreateSectionCommand request, CancellationToken cancellationToken)
        {
            if (request.Dto == null)
                return new Response<string>("Section data is required.", false) { StatusCode = System.Net.HttpStatusCode.BadRequest };

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
                return new Response<string>("Failed to create section.", false) { StatusCode = System.Net.HttpStatusCode.InternalServerError };

            return new Response<string>("Section created successfully.") { StatusCode = System.Net.HttpStatusCode.Created, Succeeded = true };
        }
    }
}
