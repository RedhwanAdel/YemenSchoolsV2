namespace YemenSchoolsV1.Application.Dto
{
    public class UpdateParentProfileDto
    {
        // AppUser
        public string? Name { get; set; }
        public string? ImageUrl { get; set; }

        // Parent
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string? Email { get; set; }
        public string? JobTitle { get; set; }
    }

}
