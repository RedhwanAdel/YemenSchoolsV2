using AutoMapper;
using YemenSchoolsV1.Application.Bases;
using MediatR;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YemenSchoolsV1.Application.Features.Cities.Commands.CreateCity;
using YemenSchoolsV1.Application.Resources;
using YemenSchoolsV1.Domain.Entities;
using YemenSchoolsV1.Application.Contracts.Persistence;

namespace YemenSchoolsV1.Application.Features.Cities.Commands.UpdateCity
{
    public class EditCityCommandHandler:ResponseHandler,IRequestHandler<EditCityCommand,Response<EditCityResponse>>
    {
        #region faild

        private readonly ICityRepository _cityRepository;
        private readonly IMapper _mapper;
        #endregion

        #region ctor
        public EditCityCommandHandler(ICityRepository cityRepository, IMapper mapper, IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            _cityRepository = cityRepository;
            _mapper = mapper;
        }

       

        public async Task<Response<EditCityResponse>> Handle(EditCityCommand request, CancellationToken cancellationToken)
        {
            if (request == null || request.Id == Guid.Empty)
            {
                return BadRequest<EditCityResponse>();
            }
            var cityDomain = _mapper.Map<City>(request);
            cityDomain = await _cityRepository.UpdateAsync(request.Id, cityDomain);
            if (cityDomain == null)
            {
                return UnprocessableEntity<EditCityResponse>();
            }

            var cityResponse = _mapper.Map<EditCityResponse>(cityDomain);
            return Success(cityResponse);
        }
        #endregion
    }
}
