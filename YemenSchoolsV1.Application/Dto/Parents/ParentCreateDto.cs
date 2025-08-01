using System.ComponentModel.DataAnnotations;
using YemenSchoolsV1.Domain.Enums;

namespace YemenSchoolsV1.Application.Dto.Parents
{
    public class ParentCreateDto
    {
        [Required]
        public string NameAr { get; set; } = string.Empty;
        [Required]
        public string NameEn { get; set; } = string.Empty;
        [Required]
        [Phone]
        public string PhoneNumber { get; set; } = string.Empty;
        [Required]
        public string Address { get; set; } = string.Empty;
        [Required]
        public string NationalId { get; set; } = string.Empty;
        [EmailAddress]
        public string? Email { get; set; }
        public Gender Gender { get; set; }
        public string? JobTitle { get; set; }
        public DateTime? DateOfBirth { get; set; }

    }
}
