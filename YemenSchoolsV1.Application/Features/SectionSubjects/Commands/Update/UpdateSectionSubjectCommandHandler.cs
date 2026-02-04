using AutoMapper;
using MediatR;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Features.SectionSubjects.Commands.Update
{
    public class UpdateSectionSubjectCommandHandler : IRequestHandler<UpdateSectionSubjectCommand, Response<string>>
    {
        private readonly ISectionSubjectRepository _repository;
        private readonly IMapper _mapper;

        public UpdateSectionSubjectCommandHandler(ISectionSubjectRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response<string>> Handle(UpdateSectionSubjectCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<SectionSubject>(request.Dto);
            entity.Id = request.Id; // Ensure ID matches URL
            
            var updated = await _repository.UpdateAsync(request.Id, entity);
            if (updated == null)
                return new Response<string>("SectionSubject not found.") { Succeeded = false, StatusCode = System.Net.HttpStatusCode.NotFound };

            return new Response<string>("SectionSubject updated successfully.");
        }
    }
}
