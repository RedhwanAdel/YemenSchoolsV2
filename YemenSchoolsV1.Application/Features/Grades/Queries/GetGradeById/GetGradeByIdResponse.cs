namespace YemenSchoolsV1.Application.Features.Grades.Queries.GetGradeById
{
    public class GetGradeByIdResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string TermName { get; set; }

        public bool IsActive { get; set; }
    }
}
