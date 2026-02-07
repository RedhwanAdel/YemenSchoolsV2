using MediatR;
using Microsoft.Extensions.Localization;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Resources;

namespace YemenSchoolsV1.Application.Features.SectionSubjects.Commands.Delete
{
    public class DeleteSectionSubjectCommandHandler : ResponseHandler, IRequestHandler<DeleteSectionSubjectCommand, Response<bool>>
    {
        private readonly ISectionSubjectRepository _repository;

        public DeleteSectionSubjectCommandHandler(
            ISectionSubjectRepository repository,
            IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            _repository = repository;
        }

        public async Task<Response<bool>> Handle(DeleteSectionSubjectCommand request, CancellationToken cancellationToken)
        {
            var deleted = await _repository.DeleteAsync(request.Id);
            if (!deleted)
                return NotFound<bool>("SectionSubject not found.");

            return Deleted<bool>();
        }
    }
}
