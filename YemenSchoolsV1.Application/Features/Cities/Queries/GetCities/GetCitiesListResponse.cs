namespace YemenSchoolsV1.Application.Features.Cities.Queries.GetCities
{
    public class GetCitiesListResponse
    {
        public Guid Id { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public string? Image { get; set; }

    }
}
