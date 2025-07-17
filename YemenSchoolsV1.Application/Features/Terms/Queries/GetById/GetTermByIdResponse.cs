namespace YemenSchoolsV1.Application.Features.Terms.Queries.GetById
{
    public class GetTermByIdResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string AcademicYearName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public bool IsActive { get; set; }
    }
}
