using MediatR;
using YemenSchoolsV1.Application.Bases;
using AutoMapper;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Dto;

namespace YemenSchoolsV1.Application.Features.Sections.Queries.GetById
{
    public class GetSectionByIdQueryHandler : IRequestHandler<GetSectionByIdQuery, Response<SectionDto>>
    {
        private readonly ISectionRepository _repository;
        private readonly IMapper _mapper;

        public GetSectionByIdQueryHandler(ISectionRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response<SectionDto>> Handle(GetSectionByIdQuery request, CancellationToken cancellationToken)
        {
            if (request.Id == Guid.Empty)
                return new Response<SectionDto>("Invalid section ID.", false) { StatusCode = System.Net.HttpStatusCode.BadRequest };

            var section = await _repository.GetSectionByIdAsync(request.Id);
            if (section == null)
                return new Response<SectionDto>("Section not found.", false) { StatusCode = System.Net.HttpStatusCode.NotFound };

            var sectionDto = _mapper.Map<SectionDto>(section);
            return new Response<SectionDto>(sectionDto);
        }
    }
}
