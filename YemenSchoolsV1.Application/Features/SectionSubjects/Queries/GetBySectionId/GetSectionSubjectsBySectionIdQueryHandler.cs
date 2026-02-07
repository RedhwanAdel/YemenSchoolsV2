using MediatR;
using Microsoft.Extensions.Localization;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Dto;
using YemenSchoolsV1.Application.Resources;

namespace YemenSchoolsV1.Application.Features.SectionSubjects.Queries.GetBySectionId
{
    public class GetSectionSubjectsBySectionIdQueryHandler : ResponseHandler, IRequestHandler<GetSectionSubjectsBySectionIdQuery, Response<List<SectionSubjectInfoDto>>>
    {
        private readonly ISectionSubjectRepository _repository;

        public GetSectionSubjectsBySectionIdQueryHandler(
            ISectionSubjectRepository repository,
            IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            _repository = repository;
        }

        public async Task<Response<List<SectionSubjectInfoDto>>> Handle(GetSectionSubjectsBySectionIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetSectionSubjectsInfoBySectionIdAsync(request.SectionId);
            return Success(result);
        }
    }
}
