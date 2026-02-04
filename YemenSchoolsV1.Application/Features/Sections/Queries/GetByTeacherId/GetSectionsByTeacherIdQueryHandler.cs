using MediatR;
using YemenSchoolsV1.Application.Bases;
using AutoMapper;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Dto;

namespace YemenSchoolsV1.Application.Features.Sections.Queries.GetByTeacherId
{
    public class GetSectionsByTeacherIdQueryHandler : IRequestHandler<GetSectionsByTeacherIdQuery, Response<IEnumerable<SectionByGradeAndYearDto>>>
    {
        private readonly ISectionRepository _repository;
        private readonly IMapper _mapper;

        public GetSectionsByTeacherIdQueryHandler(ISectionRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response<IEnumerable<SectionByGradeAndYearDto>>> Handle(GetSectionsByTeacherIdQuery request, CancellationToken cancellationToken)
        {
             if (request.TeacherId == Guid.Empty)
                return new Response<IEnumerable<SectionByGradeAndYearDto>>("Invalid teacher ID.", false) { StatusCode = System.Net.HttpStatusCode.BadRequest };

            var sections = await _repository.GetSectionsByTeacherIdAsync(request.TeacherId);
            var sectionDtos = _mapper.Map<IEnumerable<SectionByGradeAndYearDto>>(sections);
            return new Response<IEnumerable<SectionByGradeAndYearDto>>(sectionDtos);
        }
    }
}
