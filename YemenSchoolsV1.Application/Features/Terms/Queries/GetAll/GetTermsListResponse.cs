namespace YemenSchoolsV1.Application.Features.Terms.Queries.GetAll
{
    public class GetTermsListResponse
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        public string AcademicYearName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
