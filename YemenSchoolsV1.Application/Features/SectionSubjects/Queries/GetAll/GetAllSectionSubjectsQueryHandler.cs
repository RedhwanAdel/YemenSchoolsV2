using AutoMapper;
using MediatR;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Dto;

namespace YemenSchoolsV1.Application.Features.SectionSubjects.Queries.GetAll
{
    public class GetAllSectionSubjectsQueryHandler : IRequestHandler<GetAllSectionSubjectsQuery, Response<List<SectionSubjectInfoDto>>>
    {
        private readonly ISectionSubjectRepository _repository;
        private readonly IMapper _mapper;

        public GetAllSectionSubjectsQueryHandler(ISectionSubjectRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response<List<SectionSubjectInfoDto>>> Handle(GetAllSectionSubjectsQuery request, CancellationToken cancellationToken)
        {
            var entities = await _repository.GetAllAsync();
            var dtos = _mapper.Map<List<SectionSubjectInfoDto>>(entities);
            return new Response<List<SectionSubjectInfoDto>>(dtos);
        }
    }
}
