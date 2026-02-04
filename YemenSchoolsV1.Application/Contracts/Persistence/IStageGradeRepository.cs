using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Contracts.Persistence
{
    public interface IStageGradeRepository : IGenericRepositoryAsync<StageGrade>
    {
        Task<List<StageGrade>> GetAllStageGradesAsync();

    }
}
