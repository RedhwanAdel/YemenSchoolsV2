using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Dto;
using YemenSchoolsV1.Application.Resources;

namespace YemenSchoolsV1.Application.Features.Sections.Queries.GetById
{
    public class GetSectionByIdQueryHandler : ResponseHandler, IRequestHandler<GetSectionByIdQuery, Response<SectionDto>>
    {
        private readonly ISectionRepository _repository;
        private readonly IMapper _mapper;

        public GetSectionByIdQueryHandler(
            ISectionRepository repository,
            IMapper mapper,
            IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response<SectionDto>> Handle(GetSectionByIdQuery request, CancellationToken cancellationToken)
        {
            if (request.Id == Guid.Empty)
                return BadRequest<SectionDto>("Invalid section ID.");

            var section = await _repository.GetSectionByIdAsync(request.Id);
            if (section == null)
                return NotFound<SectionDto>("Section not found.");

            var sectionDto = _mapper.Map<SectionDto>(section);
            return Success(sectionDto);
        }
    }
}
