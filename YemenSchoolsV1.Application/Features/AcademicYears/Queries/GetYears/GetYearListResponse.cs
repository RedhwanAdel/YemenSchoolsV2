namespace YemenSchoolsV1.Application.Features.AcademicYears.Queries.GetYears
{
    public class GetYearListResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Guid SchoolId { get; set; }
        public bool IsCurrentYear { get; set; }

    }
}
