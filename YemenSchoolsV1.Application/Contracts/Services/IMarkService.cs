using YemenSchoolsV1.Application.Dto.Marks;

namespace YemenSchoolsV1.Application.Contracts.Services
{
    public interface IMarkService
    {
        Task<IEnumerable<SectionSubjectDto>> GetTeacherSectionSubjectsAsync(Guid teacherId);

        // لإدخال درجات مجموعة من الطلاب في مادة معينة
        Task CreateMarksAsync(Guid teacherId, Guid sectionSubjectId, string assessmentType, Dictionary<Guid, double> studentScores);

        // لتحديث درجات مجموعة من الطلاب في مادة معينة
        Task UpdateMarksAsync(Guid teacherId, Guid sectionSubjectId, string assessmentType, Dictionary<Guid, double> studentScores);

        // لجلب كشف درجات طالب واحد
        Task<StudentTranscriptDto> GetStudentTranscriptAsync(Guid studentId);

        // لجلب تقرير شامل لدرجات شعبة في مادة معينة
        Task<SectionMarkReportDto> GetSectionMarkReportAsync(Guid sectionSubjectId);
    }
}
