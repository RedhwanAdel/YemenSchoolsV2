using AutoMapper;
using MediatR;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Dto;

namespace YemenSchoolsV1.Application.Features.SectionSubjects.Queries.GetById
{
    public class GetSectionSubjectByIdQueryHandler : IRequestHandler<GetSectionSubjectByIdQuery, Response<SectionSubjectInfoDto>>
    {
        private readonly ISectionSubjectRepository _repository;
        private readonly IMapper _mapper;

        public GetSectionSubjectByIdQueryHandler(ISectionSubjectRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response<SectionSubjectInfoDto>> Handle(GetSectionSubjectByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id);
            if (entity == null)
                return new Response<SectionSubjectInfoDto>("SectionSubject not found.") { Succeeded = false, StatusCode = System.Net.HttpStatusCode.NotFound };

            var dto = _mapper.Map<SectionSubjectInfoDto>(entity);
            return new Response<SectionSubjectInfoDto>(dto);
        }
    }
}
