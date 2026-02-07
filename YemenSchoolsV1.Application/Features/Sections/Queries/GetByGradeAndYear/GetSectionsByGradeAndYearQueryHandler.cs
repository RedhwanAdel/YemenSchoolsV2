using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Dto;
using YemenSchoolsV1.Application.Resources;

namespace YemenSchoolsV1.Application.Features.Sections.Queries.GetByGradeAndYear
{
    public class GetSectionsByGradeAndYearQueryHandler : ResponseHandler, IRequestHandler<GetSectionsByGradeAndYearQuery, Response<IEnumerable<SectionByGradeAndYearDto>>>
    {
        private readonly ISectionRepository _repository;
        private readonly IMapper _mapper;

        public GetSectionsByGradeAndYearQueryHandler(
            ISectionRepository repository,
            IMapper mapper,
            IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response<IEnumerable<SectionByGradeAndYearDto>>> Handle(GetSectionsByGradeAndYearQuery request, CancellationToken cancellationToken)
        {
            if (request.AcademicYearId == Guid.Empty || request.SchoolGradeId == Guid.Empty)
                return BadRequest<IEnumerable<SectionByGradeAndYearDto>>("Invalid academic year or school grade ID.");

            var sections = await _repository.GetSectionsByAcademicYearAndSchoolGradeAsync(request.AcademicYearId, request.SchoolGradeId);
            var sectionDtos = _mapper.Map<IEnumerable<SectionByGradeAndYearDto>>(sections);
            return Success(sectionDtos);
        }
    }
}
