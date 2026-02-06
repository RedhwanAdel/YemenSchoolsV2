namespace YemenSchoolsV1.Application.Dto
{
    public class UserInfoDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? ImageUrl { get; set; }
        public string? Email { get; set; }
        public Guid EntityId { get; set; }
        public string UserType { get; set; } = string.Empty;
        public Guid? SchoolId { get; set; }
    }
}
