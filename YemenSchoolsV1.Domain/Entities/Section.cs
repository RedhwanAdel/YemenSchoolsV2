namespace YemenSchoolsV1.Domain.Entities
{
    public class Section
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public Guid AcademicYearId { get; set; }
        public Guid SchoolGradeId { get; set; }
        public int Capacity { get; set; }


        // Navigation Property
        public SchoolGrade SchoolGrade { get; set; } = null!;
        public AcademicYear AcademicYear { get; set; } = null!;
        public ICollection<SectionSubject> SectionSubjects { get; set; } = [];


    }
}
