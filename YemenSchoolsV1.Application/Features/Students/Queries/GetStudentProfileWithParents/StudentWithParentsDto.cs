using YemenSchoolsV1.Domain.Enums;

namespace YemenSchoolsV1.Application.Features.Students.Queries.GetStudentProfileWithParents
{
    public class StudentWithParentsDto
    {

        public Guid Id { get; set; }
        public string RegisterNo { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public string Nationality { get; set; }
        public string Address { get; set; }
        public Gender Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public ICollection<ParentSummaryDto> Parents { get; set; }
    }
}
