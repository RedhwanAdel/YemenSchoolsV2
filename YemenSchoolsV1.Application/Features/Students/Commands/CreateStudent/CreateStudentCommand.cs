using MediatR;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Domain.Enums;

namespace YemenSchoolsV1.Application.Features.Students.Commands.CreateStudent
{
    public class CreateStudentCommand : IRequest<Response<Guid>>
    {
        public required string NameAr { get; set; }
        public required string NameEn { get; set; }
        public required string Nationality { get; set; }
        public required string Address { get; set; }
        public Gender Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }

        public required string RegisterNo { get; set; }

        public Guid SchoolId { get; set; }
        public Guid CurrentAcademicYearId { get; set; }
        public Guid CurrentSectionId { get; set; }

        public List<ParentAssociationDto> Parents { get; set; } = new();
    }
}
