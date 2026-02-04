using YemenSchoolsV1.Application.Dto;

namespace YemenSchoolsV1.Application.Contracts
{
    public interface IStudentReportService
    {
        byte[] GenerateStudentReport(StudentReportDto dto);

    }
}
