namespace YemenSchoolsV1.Application.Dto
{
    public class StudentReportDto
    {
        // بيانات المدرسة
        public Guid SchoolId { get; set; }
        public string SchoolName { get; set; } = null!;
        public string? SchoolLogoUrl { get; set; }

        // بيانات الطالب
        public Guid StudentId { get; set; }
        public string StudentNameAr { get; set; } = null!;
        public string StudentNameEn { get; set; } = null!;
        public string GradeName { get; set; } = null!;
        public string SectionName { get; set; } = null!;
        public string StageName { get; set; } = null!;
        public string? ProfileImage { get; set; }

        // درجات الطالب
        public List<StudentSubjectReportDto> Subjects { get; set; } = new();

        // حضور وغياب
        public int TotalAttendanceDays { get; set; }
        public int TotalAbsenceDays { get; set; }
        public double AttendancePercentage { get; set; } // مثلا: 95%
    }
}
