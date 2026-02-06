using MediatR;
using Microsoft.Extensions.Localization;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Resources;

namespace YemenSchoolsV1.Application.Features.Subjects.Commands.Delete
{
    public class DeleteSubjectCommandHandler : ResponseHandler, IRequestHandler<DeleteSubjectCommand, Response<bool>>
    {
        private readonly ISubjectRepository _subjectRepository;

        public DeleteSubjectCommandHandler(ISubjectRepository subjectRepository, IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            _subjectRepository = subjectRepository;
        }

        public async Task<Response<bool>> Handle(DeleteSubjectCommand request, CancellationToken cancellationToken)
        {
            var subjectEntity = await _subjectRepository.GetByIdAsync(request.Id);
            if (subjectEntity == null)
            {
                return NotFound<bool>();
            }

            var deleted = await _subjectRepository.DeleteAsync(request.Id);
            if (!deleted)
            {
                return UnprocessableEntity<bool>();
            }
            return Deleted<bool>();
        }
    }
}
