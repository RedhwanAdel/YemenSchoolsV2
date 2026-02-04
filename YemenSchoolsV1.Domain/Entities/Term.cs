namespace YemenSchoolsV1.Domain.Entities
{
    public class Term
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public Guid AcademicYearId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        // Navigation Property
        public AcademicYear AcademicYear { get; set; } = null!;
        public ICollection<SectionSubject> SectionSubjects { get; set; } = [];

    }
}
