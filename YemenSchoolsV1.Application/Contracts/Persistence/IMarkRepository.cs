using FinalProject.Application.Contracts.Persistence;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Contracts.Persistence
{
    public interface IMarkRepository : IGenericRepositoryAsync<Mark>
    {


        // لإضافة مجموعة من الدرجات (مفيد عند إدخال درجات شعبة كاملة)
        Task AddMarksAsync(IEnumerable<Mark> marks);

        // لجلب درجة معينة حسب معرفها
        Task<Mark?> GetMarkByIdAsync(Guid markId);

        // لجلب جميع درجات طالب معين
        Task<IEnumerable<Mark>> GetMarksByStudentIdAsync(Guid studentId);

        // لجلب جميع درجات مادة معينة في شعبة محددة
        Task<IEnumerable<Mark>> GetMarksBySectionSubjectAsync(Guid sectionSubjectId);


    }
}
