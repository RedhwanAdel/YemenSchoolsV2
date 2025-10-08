namespace YemenSchoolsV1.Application.Dto
{
    public class StudentSubjectReportDto
    {
        public string SubjectName { get; set; } = null!;
        public int Score { get; set; }
        public string Grade { get; set; } = null!; // ممتاز، جيد جدًا، إلخ
        public SubjectDetailsDto Details { get; set; } = new SubjectDetailsDto();
    }
}
