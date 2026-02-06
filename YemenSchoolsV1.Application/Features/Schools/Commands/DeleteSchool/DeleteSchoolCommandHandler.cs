using MediatR;
using Microsoft.Extensions.Localization;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Resources;

namespace YemenSchoolsV1.Application.Features.Schools.Commands.DeleteSchool
{
    public class DeleteSchoolCommandHandler : ResponseHandler, IRequestHandler<DeleteSchoolCommand, Response<bool>>
    {
        private readonly ISchoolRepository _schoolRepository;

        public DeleteSchoolCommandHandler(ISchoolRepository schoolRepository, IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            _schoolRepository = schoolRepository;
        }

        public async Task<Response<bool>> Handle(DeleteSchoolCommand request, CancellationToken cancellationToken)
        {
            var schoolEntity = await _schoolRepository.GetByIdAsync(request.Id);
            if (schoolEntity == null)
            {
                return NotFound<bool>();
            }

            var deleted = await _schoolRepository.DeleteAsync(request.Id);
            if (!deleted)
            {
                return UnprocessableEntity<bool>();
            }
            return Deleted<bool>();
        }
    }
}
