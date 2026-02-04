namespace YemenSchoolsV1.Domain.Entities
{
    public class SchoolGrade
    {
        public Guid Id { get; set; }
        public Guid SchoolId { get; set; }
        public Guid StageGradeId { get; set; }
        public bool IsActive { get; set; } = true;



        // Navigation Property
        public School School { get; set; } = null!;
        public StageGrade StageGrade { get; set; } = null!;
        public ICollection<Section> Sections { get; set; } = [];
        public ICollection<GradeSubject> GradeSubjects { get; set; } = [];


    }
}
