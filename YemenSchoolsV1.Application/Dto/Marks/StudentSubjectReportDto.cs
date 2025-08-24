namespace YemenSchoolsV1.Application.Dto.Marks
{
    public class StudentSubjectReportDto
    {
        public string Name { get; set; } = string.Empty;
        public int Score { get; set; }
        public string Grade { get; set; } = string.Empty;
        public SubjectDetailsDto Details { get; set; } = new();
    }
    public class SubjectDetailsDto
    {
        public List<GradeItemDto> Grades { get; set; } = new();
    }

    public class GradeItemDto
    {
        public string Type { get; set; } = string.Empty;
        public double Score { get; set; }
        public double Total { get; set; }
        public string Percentage { get; set; } = string.Empty;
    }

}