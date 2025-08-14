namespace YemenSchoolsV1.Domain.Entities
{
    public class AcademicYear
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public bool IsCurrentYear { get; set; } = false;

        public Guid SchoolId { get; set; }

        // Navigation Property
        public School School { get; set; } = null!;
        public ICollection<Term> Terms { get; set; } = [];
        public ICollection<Section> Sections { get; set; } = [];
        public ICollection<Student> Students { get; set; } = [];
        public ICollection<Attendance> Attendances { get; set; } = [];



    }
}
