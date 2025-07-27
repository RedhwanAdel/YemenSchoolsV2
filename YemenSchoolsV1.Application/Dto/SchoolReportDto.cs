namespace YemenSchoolsV1.Application.Dto
{
    public class SchoolReportDto
    {
        public Guid SchoolId { get; set; }
        public string NameAr { get; set; } = string.Empty;
        public string NameEn { get; set; } = string.Empty;
        public string? DescriptionAr { get; set; }
        public string AddressAr { get; set; } = string.Empty;
        public string? PostalCode { get; set; }
        public string? MainPhone { get; set; }
        public string? Email { get; set; }
        public int SchoolType { get; set; }
        public int SchoolLevel { get; set; }
        public int GenderType { get; set; }
        public int CurriculumType { get; set; }

        // Location (Arabic names only)
        public Guid CityId { get; set; }
        public string? CityNameAr { get; set; }
        public Guid RegionId { get; set; }
        public string? RegionNameAr { get; set; }

        // Phones
        public List<string> PhoneNumbers { get; set; } = new();

        // Counts
        public int TeachersCount { get; set; }
        public int StudentsCount { get; set; }
        public int GradesCount { get; set; }
        public int SubjectsCount { get; set; }
        public int SectionsCount { get; set; }
        public int AcademicYearsCount { get; set; }
        public int NewsCount { get; set; }
        public int PhotosCount { get; set; }
        public int ParentsCount { get; set; }
        public int RatingsCount { get; set; }

    }
}
