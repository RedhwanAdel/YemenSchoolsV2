using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Resources;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Features.SectionSubjects.Commands.Update
{
    public class UpdateSectionSubjectCommandHandler : ResponseHandler, IRequestHandler<UpdateSectionSubjectCommand, Response<string>>
    {
        private readonly ISectionSubjectRepository _repository;
        private readonly IMapper _mapper;

        public UpdateSectionSubjectCommandHandler(
            ISectionSubjectRepository repository,
            IMapper mapper,
            IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
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
                return NotFound<string>("SectionSubject not found.");

            return Success("SectionSubject updated successfully.");
        }
    }
}
