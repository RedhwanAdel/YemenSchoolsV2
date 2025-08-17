using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Domain.Commons;
using YemenSchoolsV1.Domain.Enums;

namespace YemenSchoolsV1.Domain.Entities
{
    public class Student : GeneralLocalizableEntity, ISchoolEntity
    {
        public Guid Id { get; set; }
        public required string RegisterNo { get; set; }
        public required string NameEn { get; set; }
        public required string NameAr { get; set; }
        public required string Nationality { get; set; }
        public required string Address { get; set; }
        public Gender Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? ProfileImage { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedTime { get; set; } = DateTime.UtcNow;

        public Guid SchoolId { get; set; }
        public Guid CurrentAcademicYearId { get; set; }
        public Guid CurrentSectionId { get; set; }
        public Guid UserId { get; set; }

        public School School { get; set; } = null!;
        public AcademicYear CurrentAcademicYear { get; set; } = null!;
        public Section CurrentSection { get; set; } = null!;
        public AppUser User { get; set; } = null!;
        public ICollection<ParentStudent> Parents { get; set; } = [];
        public ICollection<AttendanceDetail> AttendanceDetails { get; set; } = [];
        public ICollection<Mark> Marks { get; set; } = [];



    }
}
