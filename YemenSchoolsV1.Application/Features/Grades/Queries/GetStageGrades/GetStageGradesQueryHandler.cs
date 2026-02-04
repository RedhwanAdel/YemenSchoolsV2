using MediatR;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Dto;
using AutoMapper;

namespace YemenSchoolsV1.Application.Features.Grades.Queries.GetStageGrades
{
    public class GetStageGradesQueryHandler : IRequestHandler<GetStageGradesQuery, Response<List<StageGradeDto>>>
    {
        private readonly IStageGradeRepository _stageGradeRepository;
        private readonly IMapper _mapper;

        public GetStageGradesQueryHandler(IStageGradeRepository stageGradeRepository, IMapper mapper)
        {
            _stageGradeRepository = stageGradeRepository;
            _mapper = mapper;
        }

        public async Task<Response<List<StageGradeDto>>> Handle(GetStageGradesQuery request, CancellationToken cancellationToken)
        {
            var stageGrades = await _stageGradeRepository.GetAllStageGradesAsync();
            var result = _mapper.Map<List<StageGradeDto>>(stageGrades);

            return new Response<List<StageGradeDto>>(result, "Success")
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Succeeded = true
            };
        }
    }
}
