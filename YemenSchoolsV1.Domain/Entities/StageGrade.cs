namespace YemenSchoolsV1.Domain.Entities
{
    public class StageGrade
    {
        public Guid Id { get; set; }
        public Guid StageId { get; set; }
        public Guid GradeId { get; set; }

        // Navigation Property
        public Stage Stage { get; set; } = null!;
        public Grade Grade { get; set; } = null!;
        public ICollection<SchoolGrade> SchoolGrades { get; set; } = [];



    }
}
