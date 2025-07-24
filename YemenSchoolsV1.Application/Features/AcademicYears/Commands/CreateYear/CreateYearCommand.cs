using FinalProject.Application.Bases;
using MediatR;

namespace YemenSchoolsV1.Application.Features.AcademicYears.Commands.CreateYear
{
    public class CreateYearCommand : IRequest<Response<string>>
    {
        public string Name { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Guid SchoolId { get; set; }
    }
}
