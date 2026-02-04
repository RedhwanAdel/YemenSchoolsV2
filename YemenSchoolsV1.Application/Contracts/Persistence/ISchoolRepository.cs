using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Dto;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Contracts.Persistence
{
    public interface ISchoolRepository : IGenericRepositoryAsync<School>
    {
        IQueryable<School> GetSchoolsWithCityAndRegionQueryable();
        Task<School?> GetSchoolDetailsInculdeAsync(Guid cityId);
        Task CreateSchoolPhonesRangAsync(List<SchoolPhone> schoolPhones);
        Task<SchoolForUpdate?> GetSchoolByIdForUpdateAsync(Guid schoolId);
        Task AssignSubjectsToSchoolGradeAsync(Guid schoolGradeId, List<Guid> subjectIds);
        Task<List<SubjectDto>> GetSubjectsForSchoolGradeAsync(Guid schoolGradeId);
        Task<SchoolReportDto?> GetSchoolReportAsync(Guid schoolId);
        Task<SchoolPhoto> AddSchoolPhotoAsync(SchoolPhoto schoolPhoto);
        Task<List<SchoolPhoto>> GetSchoolPhotosAsync(Guid schoolId);



    }
}
