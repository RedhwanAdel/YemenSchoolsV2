namespace YemenSchoolsV1.Application.Dto
{
    public class AssignSubjectsToSchoolGradeDto
    {
        public Guid SchoolGradeId { get; set; }
        public List<Guid> SubjectIds { get; set; } = [];
    }
}
