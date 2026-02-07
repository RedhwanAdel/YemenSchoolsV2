using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Dto;
using YemenSchoolsV1.Application.Resources;

namespace YemenSchoolsV1.Application.Features.Grades.Queries.GetStageGrades
{
    public class GetStageGradesQueryHandler : ResponseHandler, IRequestHandler<GetStageGradesQuery, Response<List<StageGradeDto>>>
    {
        private readonly IStageGradeRepository _stageGradeRepository;
        private readonly IMapper _mapper;

        public GetStageGradesQueryHandler(
            IStageGradeRepository stageGradeRepository,
            IMapper mapper,
            IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            _stageGradeRepository = stageGradeRepository;
            _mapper = mapper;
        }

        public async Task<Response<List<StageGradeDto>>> Handle(GetStageGradesQuery request, CancellationToken cancellationToken)
        {
            var stageGrades = await _stageGradeRepository.GetAllStageGradesAsync();
            var result = _mapper.Map<List<StageGradeDto>>(stageGrades);

            return Success(result);
        }
    }
}
