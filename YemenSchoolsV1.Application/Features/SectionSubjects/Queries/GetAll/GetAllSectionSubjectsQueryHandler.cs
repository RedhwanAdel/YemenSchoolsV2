using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Dto;
using YemenSchoolsV1.Application.Resources;

namespace YemenSchoolsV1.Application.Features.SectionSubjects.Queries.GetAll
{
    public class GetAllSectionSubjectsQueryHandler : ResponseHandler, IRequestHandler<GetAllSectionSubjectsQuery, Response<List<SectionSubjectInfoDto>>>
    {
        private readonly ISectionSubjectRepository _repository;
        private readonly IMapper _mapper;

        public GetAllSectionSubjectsQueryHandler(
            ISectionSubjectRepository repository,
            IMapper mapper,
            IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response<List<SectionSubjectInfoDto>>> Handle(GetAllSectionSubjectsQuery request, CancellationToken cancellationToken)
        {
            var entities = await _repository.GetAllAsync();
            var dtos = _mapper.Map<List<SectionSubjectInfoDto>>(entities);
            return Success(dtos);
        }
    }
}
