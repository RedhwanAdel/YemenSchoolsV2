using YemenSchoolsV1.Domain.Enums;

namespace YemenSchoolsV1.Domain.Entities
{
    public class Parent
    {
        public Guid Id { get; set; }
        public required string NameAr { get; set; }
        public required string NameEn { get; set; }
        public required string PhoneNumber { get; set; }
        public required string Address { get; set; }
        public required string NationalId { get; set; }
        public string? Email { get; set; }
        public Gender Gender { get; set; }
        public string? JobTitle { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime? DateOfBirth { get; set; }


        public Guid UserId { get; set; }
        public AppUser User { get; set; } = null!;

        public ICollection<ParentStudent> Students { get; set; } = [];

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
