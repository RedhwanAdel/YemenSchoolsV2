using MediatR;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Dto;

namespace YemenSchoolsV1.Application.Features.SectionSubjects.Queries.GetBySectionId
{
    public class GetSectionSubjectsBySectionIdQueryHandler : IRequestHandler<GetSectionSubjectsBySectionIdQuery, Response<List<SectionSubjectInfoDto>>>
    {
        private readonly ISectionSubjectRepository _repository;

        public GetSectionSubjectsBySectionIdQueryHandler(ISectionSubjectRepository repository)
        {
            _repository = repository;
        }

        public async Task<Response<List<SectionSubjectInfoDto>>> Handle(GetSectionSubjectsBySectionIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetSectionSubjectsInfoBySectionIdAsync(request.SectionId);
            return new Response<List<SectionSubjectInfoDto>>(result);
        }
    }
}
