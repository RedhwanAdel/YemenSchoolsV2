using FinalProject.Application.Bases;
using MediatR;

namespace YemenSchoolsV1.Application.Features.AcademicYears.Commands.UpdateYear
{
    public class EditYearCommand : IRequest<Response<string>>
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Guid SchoolId { get; set; }
    }
}
