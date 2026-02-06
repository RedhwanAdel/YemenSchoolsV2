using MediatR;
using Microsoft.Extensions.Localization;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Resources;

namespace YemenSchoolsV1.Application.Features.Teachers.Commands.Delete
{
    public class DeleteTeacherCommandHandler : ResponseHandler, IRequestHandler<DeleteTeacherCommand, Response<bool>>
    {
        private readonly ITeacherRepository _teacherRepository;

        public DeleteTeacherCommandHandler(ITeacherRepository teacherRepository, IStringLocalizer<SharedResources> stringLocalizer)
            : base(stringLocalizer)
        {
            _teacherRepository = teacherRepository;
        }

        public async Task<Response<bool>> Handle(DeleteTeacherCommand request, CancellationToken cancellationToken)
        {
            var teacherEntity = await _teacherRepository.GetByIdAsync(request.Id);
            if (teacherEntity == null)
            {
                return NotFound<bool>();
            }

            var teacherDeleted = await _teacherRepository.DeleteAsync(request.Id);
            if (!teacherDeleted)
            {
                return UnprocessableEntity<bool>();
            }
            return Deleted<bool>();
        }
    }
}
