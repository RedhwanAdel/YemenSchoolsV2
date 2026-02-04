namespace YemenSchoolsV1.Domain.Entities
{
    public class Subject
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public ICollection<GradeSubject> GradeSubjects { get; set; } = [];

    }
}
