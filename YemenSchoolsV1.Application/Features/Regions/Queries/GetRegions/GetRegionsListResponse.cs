namespace YemenSchoolsV1.Application.Features.Regions.Queries.GetRegions
{
    public class GetRegionsListResponse
    {
        public Guid Id { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public string? Image { get; set; }
        public string CityName { get; set; }

        public Guid CityId { get; set; }
        public int? countSchools { get; set; }

    }
}
