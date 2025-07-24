namespace YemenSchoolsV1.Application.Features.Terms.Queries.GetByYearId
{
    public class GetTermByYearIdResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string AcademicYearName { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
