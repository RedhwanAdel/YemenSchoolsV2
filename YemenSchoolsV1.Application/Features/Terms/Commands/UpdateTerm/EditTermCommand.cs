using FinalProject.Application.Bases;
using MediatR;

namespace YemenSchoolsV1.Application.Features.Terms.Commands.UpdateTerm
{
    public class EditTermCommand : IRequest<Response<string>>
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public Guid AcademicYearId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
