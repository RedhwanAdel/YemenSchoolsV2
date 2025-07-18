namespace YemenSchoolsV1.Application.Features.Sections.Queries.GetAll
{
    public class GetSectionsListResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string GradeName { get; set; }

        public int? RoomNumber { get; set; }
    }
}
