using FinalProject.Application.Contracts.Persistence;
using YemenSchoolsV1.API.Dto;
using YemenSchoolsV1.Application.Dto;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Contracts.Persistence
{
    public interface ISchoolGradeRepository : IGenericRepositoryAsync<SchoolGrade>
    {
        Task SyncSchoolStageGradesAsync(Guid schoolId, List<Guid> stageGradeIds);
        Task<List<StageGradeDto>> GetStageGradesForSchoolAsync(Guid schoolId);
        Task<List<SchoolGradeDto>> GetSchoolGradesAsync(Guid schoolId);



    }
}
