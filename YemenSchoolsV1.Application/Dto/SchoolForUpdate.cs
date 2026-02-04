namespace YemenSchoolsV1.Application.Dto
{
    public class SchoolForUpdate
    {
        public Guid Id { get; set; }
        public string? NameAr { get; set; }
        public string? NameEn { get; set; }
        public string? AddressAr { get; set; }
        public string? AddressEn { get; set; }
        public string? PostalCode { get; set; }
        public string? MainPhone { get; set; }
        public string? Email { get; set; }
        public int SchoolType { get; set; }
        public int GenderType { get; set; }
        public int CurriculumType { get; set; }
        public int SchoolLevel { get; set; }

        public Guid CityId { get; set; }
        public string CityName { get; set; } = string.Empty;

        public Guid RegionId { get; set; }
        public string RegionName { get; set; } = string.Empty;

        public List<string> PhoneNumberList { get; set; } = [];
    }
}
