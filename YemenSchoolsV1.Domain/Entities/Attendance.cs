namespace YemenSchoolsV1.Domain.Entities
{
    public class Attendance
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public bool IsDayOff { get; set; }

        public Guid SectionId { get; set; } // يربط هذا السجل بالشعبة
        public Guid ClassTeacherId { get; set; } // يربط هذا السجل بمربي الصف
        public Guid AcademicYearId { get; set; } // يربط بالعام الدراسي

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation Property
        public Section Section { get; set; } = null!;
        public Teacher ClassTeacher { get; set; } = null!;
        public AcademicYear AcademicYear { get; set; } = null!;
        public ICollection<AttendanceDetail> AttendanceDetails { get; set; } = [];
    }
}
