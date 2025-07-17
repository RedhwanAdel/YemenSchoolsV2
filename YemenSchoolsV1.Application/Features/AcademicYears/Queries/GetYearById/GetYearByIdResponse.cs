namespace YemenSchoolsV1.Application.Features.AcademicYears.Queries.GetYearById
{
    public class GetYearByIdResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; } = true;

        public string StageName { get; set; }
    }
}
