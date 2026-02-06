using MediatR;
using Microsoft.Extensions.Localization;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Resources;

namespace YemenSchoolsV1.Application.Features.Cities.Commands.DeleteCity
{
    public class DeleteCityCommandHandler : ResponseHandler, IRequestHandler<DeleteCityCommand, Response<bool>>
    {
        private readonly ICityRepository _cityRepository;

        public DeleteCityCommandHandler(IStringLocalizer<SharedResources> stringLocalizer, ICityRepository cityRepository) : base(stringLocalizer)
        {
            _cityRepository = cityRepository;
        }

        public async Task<Response<bool>> Handle(DeleteCityCommand request, CancellationToken cancellationToken)
        {
            var city = await _cityRepository.GetByIdAsync(request.Id);
            if (city == null)
            {
                return NotFound<bool>();
            }
            var deleted = await _cityRepository.DeleteAsync(request.Id);
            if (!deleted)
            {
                return UnprocessableEntity<bool>();
            }
            return Deleted<bool>();
        }
    }
}
