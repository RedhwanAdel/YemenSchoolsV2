using YemenSchoolsV1.Application.Dto;

namespace YemenSchoolsV1.Application.Contracts
{
    public interface ISchoolReportService
    {
        byte[] GenerateSchoolReport(SchoolReportDto dto);

    }
}
