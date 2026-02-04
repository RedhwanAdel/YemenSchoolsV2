namespace YemenSchoolsV1.Application.Features.Students.Queries.GetStudentProfileWithParents
{
    public class ParentSummaryDto
    {
        public Guid ParentId { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public string RelationType { get; set; }
    }
}
