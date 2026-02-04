using MediatR;
using YemenSchoolsV1.Application.Bases;
using AutoMapper;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Dto;

namespace YemenSchoolsV1.Application.Features.Sections.Queries.GetByGradeAndYear
{
    public class GetSectionsByGradeAndYearQueryHandler : IRequestHandler<GetSectionsByGradeAndYearQuery, Response<IEnumerable<SectionByGradeAndYearDto>>>
    {
        private readonly ISectionRepository _repository;
        private readonly IMapper _mapper;

        public GetSectionsByGradeAndYearQueryHandler(ISectionRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response<IEnumerable<SectionByGradeAndYearDto>>> Handle(GetSectionsByGradeAndYearQuery request, CancellationToken cancellationToken)
        {
            if (request.AcademicYearId == Guid.Empty || request.SchoolGradeId == Guid.Empty)
                return new Response<IEnumerable<SectionByGradeAndYearDto>>("Invalid academic year or school grade ID.", false) { StatusCode = System.Net.HttpStatusCode.BadRequest };

            var sections = await _repository.GetSectionsByAcademicYearAndSchoolGradeAsync(request.AcademicYearId, request.SchoolGradeId);
            var sectionDtos = _mapper.Map<IEnumerable<SectionByGradeAndYearDto>>(sections);
            return new Response<IEnumerable<SectionByGradeAndYearDto>>(sectionDtos);
        }
    }
}
