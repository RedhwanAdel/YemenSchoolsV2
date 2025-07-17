using AutoMapper;
using FinalProject.Application.Bases;
using MediatR;
using Microsoft.Extensions.Localization;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Resources;

namespace YemenSchoolsV1.Application.Features.Grades.Queries.GetGradeById
{
    public class GetGradeByIdQuearyHandler : ResponseHandler, IRequestHandler<GetGradeByIdQueary, Response<GetGradeByIdResponse>>
    {
        private readonly IGradeRepositry gradeRepositry;
        #region faild

        private readonly IMapper mapper;
        private readonly IStringLocalizer<SharedResources> stringLocalizer;
        #endregion

        #region ctor
        public GetGradeByIdQuearyHandler(IGradeRepositry gradeRepositry, IMapper mapper, IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            this.gradeRepositry = gradeRepositry;
            this.mapper = mapper;
            this.stringLocalizer = stringLocalizer;

        }

        #endregion
        public async Task<Response<GetGradeByIdResponse>> Handle(GetGradeByIdQueary request, CancellationToken cancellationToken)
        {
            var grade = await gradeRepositry.GetGradeByIdIncludeAsync(request.Id);
            if (grade == null)
            {
                return NotFound<GetGradeByIdResponse>();
            }
            var result = mapper.Map<GetGradeByIdResponse>(grade);
            return Success(result);
        }

    }
}
