namespace YemenSchoolsV1.Domain.Entities
{
    public class GradeSubject
    {
        public Guid Id { get; set; }

        public Guid SchoolGradeId { get; set; }
        public Guid SubjectId { get; set; }

        // Navigation property 
        public SchoolGrade SchoolGrade { get; set; } = null!;
        public Subject Subject { get; set; } = null!;
        public ICollection<SectionSubject> SectionSubjects { get; set; } = [];

    }
}
