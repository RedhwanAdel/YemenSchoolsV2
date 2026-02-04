namespace YemenSchoolsV1.Domain.Entities
{
    public class SectionSubject
    {
        public Guid Id { get; set; }
        public Guid SectionId { get; set; }
        public Guid GradeSubjectId { get; set; }
        public Guid TermId { get; set; }
        public Guid? TeacherId { get; set; }
        // Navigation property 
        public Section Section { get; set; } = null!;
        public GradeSubject GradeSubject { get; set; } = null!;
        public Term Term { get; set; } = null!;
        public Teacher Teacher { get; set; } = null!;
        public ICollection<Mark> Marks { get; set; } = [];
        public ICollection<DailyLog> DailyLogs { get; set; } = [];



    }
}
