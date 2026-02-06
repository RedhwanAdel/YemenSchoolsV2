using MediatR;
using Microsoft.Extensions.Localization;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Resources;

namespace YemenSchoolsV1.Application.Features.AcademicYears.Commands.DeleteYear
{
    public class DeleteYearCommandHandler : ResponseHandler, IRequestHandler<DeleteYearCommand, Response<bool>>
    {
        private readonly IAcademicYearRepository _academicYearRepository;

        public DeleteYearCommandHandler(IStringLocalizer<SharedResources> stringLocalizer, IAcademicYearRepository academicYearRepository) : base(stringLocalizer)
        {
            _academicYearRepository = academicYearRepository;
        }

        public async Task<Response<bool>> Handle(DeleteYearCommand request, CancellationToken cancellationToken)
        {
            var yearEntity = await _academicYearRepository.GetByIdAsync(request.Id);
            if (yearEntity == null)
            {
                return NotFound<bool>();
            }

            var deleted = await _academicYearRepository.DeleteAsync(request.Id);
            if (!deleted)
            {
                return UnprocessableEntity<bool>();
            }
            return Deleted<bool>();
        }
    }
}
