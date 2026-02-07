using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Dto;
using YemenSchoolsV1.Application.Resources;

namespace YemenSchoolsV1.Application.Features.Sections.Queries.GetByTeacherId
{
    public class GetSectionsByTeacherIdQueryHandler : ResponseHandler, IRequestHandler<GetSectionsByTeacherIdQuery, Response<IEnumerable<SectionByGradeAndYearDto>>>
    {
        private readonly ISectionRepository _repository;
        private readonly IMapper _mapper;

        public GetSectionsByTeacherIdQueryHandler(
            ISectionRepository repository,
            IMapper mapper,
            IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response<IEnumerable<SectionByGradeAndYearDto>>> Handle(GetSectionsByTeacherIdQuery request, CancellationToken cancellationToken)
        {
             if (request.TeacherId == Guid.Empty)
                return BadRequest<IEnumerable<SectionByGradeAndYearDto>>("Invalid teacher ID.");

            var sections = await _repository.GetSectionsByTeacherIdAsync(request.TeacherId);
            var sectionDtos = _mapper.Map<IEnumerable<SectionByGradeAndYearDto>>(sections);
            return Success(sectionDtos);
        }
    }
}
