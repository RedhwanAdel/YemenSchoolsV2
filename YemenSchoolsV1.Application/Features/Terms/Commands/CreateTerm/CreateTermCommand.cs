using FinalProject.Application.Bases;
using MediatR;

namespace YemenSchoolsV1.Application.Features.Terms.Commands.CreateTerm
{
    public class CreateTermCommand : IRequest<Response<string>>
    {
        public string Name { get; set; } = string.Empty;
        public Guid AcademicYearId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
