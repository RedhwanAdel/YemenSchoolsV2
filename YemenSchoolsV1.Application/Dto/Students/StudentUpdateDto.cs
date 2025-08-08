using System.ComponentModel.DataAnnotations;

namespace YemenSchoolsV1.Application.Dto.Students
{
    public class StudentUpdateDto
    {
        public string? NameAr { get; set; }
        public string? NameEn { get; set; }

        [Phone(ErrorMessage = "Invalid phone number format.")]
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string? Email { get; set; }
        public string? Nationality { get; set; }




    }
}
