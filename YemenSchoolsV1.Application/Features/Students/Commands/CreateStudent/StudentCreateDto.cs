using System.ComponentModel.DataAnnotations;
using YemenSchoolsV1.Domain.Enums;

namespace YemenSchoolsV1.Application.Features.Students.Commands.CreateStudent
{
    public class StudentCreateDto
    {
        [Required(ErrorMessage = "RegisterNo is required.")]
        public string RegisterNo { get; set; }


        [Required(ErrorMessage = "NameAr is required.")]
        public string NameAr { get; set; }


        [Required(ErrorMessage = "NameEn is required.")]
        public string NameEn { get; set; }


        [Phone(ErrorMessage = "Invalid phone number.")]
        public string PhoneNumber { get; set; }


        [Required(ErrorMessage = "Address is required.")]
        public string Address { get; set; }
        public string? Email { get; set; }
        public string Nationality { get; set; }


        [Required(ErrorMessage = "Date of Birth is required.")]
        public DateTime DateOfBirth
        {
            get; set;
        }

        [Required(ErrorMessage = "Gender is required.")]
        public Gender Gender { get; set; }

        [Required]
        public required Guid SchoolId { get; set; }
        [Required(ErrorMessage = "Current Academic Year Id is required.")]
        public Guid CurrentAcademicYearId { get; set; }

        [Required(ErrorMessage = "Current Section Id is required.")]
        public Guid CurrentSectionId { get; set; }

        // Optional: Use MinLength if you want at least one parent
        public List<ParentAssociationDto>? Parents { get; set; }
    }
}
