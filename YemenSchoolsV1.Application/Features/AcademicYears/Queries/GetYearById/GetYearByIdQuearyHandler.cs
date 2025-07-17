using AutoMapper;
using FinalProject.Application.Bases;
using MediatR;
using Microsoft.Extensions.Localization;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Resources;

namespace YemenSchoolsV1.Application.Features.AcademicYears.Queries.GetYearById
{
    public class GetYearByIdQuearyHandler : ResponseHandler, IRequestHandler<GetYearByIdQueary, Response<GetYearByIdResponse>>
    {
        #region faild
        private readonly IAcademicYearRepository academicYearRepository;

        private readonly IMapper mapper;
        private readonly IStringLocalizer<SharedResources> stringLocalizer;
        #endregion

        #region ctor
        public GetYearByIdQuearyHandler(IAcademicYearRepository academicYearRepository, IMapper mapper, IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            this.academicYearRepository = academicYearRepository;
            this.mapper = mapper;
            this.stringLocalizer = stringLocalizer;
        }

        #endregion

        public async Task<Response<GetYearByIdResponse>> Handle(GetYearByIdQueary request, CancellationToken cancellationToken)
        {
            var year = await academicYearRepository.GetAcadmicYearByIdIncludeAsync(request.Id);
            if (year == null)
            {
                return NotFound<GetYearByIdResponse>();
            }
            var result = mapper.Map<GetYearByIdResponse>(year);
            return Success(result);
        }
    }
}
