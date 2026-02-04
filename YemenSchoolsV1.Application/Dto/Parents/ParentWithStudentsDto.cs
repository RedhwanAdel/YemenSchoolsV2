namespace YemenSchoolsV1.Application.Dto.Parents
{
    public class ParentWithStudentsDto
    {
        public Guid Id { get; set; }
        public required string NationalId { get; set; }
        public required string NameAr { get; set; }
        public required string NameEn { get; set; }
        public required string PhoneNumber { get; set; }
        public string? Email { get; set; }
        public required string Address { get; set; }
        public string? JobTitle { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public bool IsActive { get; set; }
        public required ICollection<StudentSummaryDto> Students { get; set; }
    }
}
