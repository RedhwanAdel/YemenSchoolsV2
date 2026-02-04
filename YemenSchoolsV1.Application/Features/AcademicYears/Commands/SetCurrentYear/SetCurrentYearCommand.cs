using MediatR;
using YemenSchoolsV1.Application.Bases;

namespace YemenSchoolsV1.Application.Features.AcademicYears.Commands.SetCurrentYear
{
    public class SetCurrentYearCommand : IRequest<Response<Guid>>
    {
        public Guid SchoolId { get; set; }
        public Guid AcademicYearId { get; set; }

        public SetCurrentYearCommand(Guid schoolId, Guid academicYearId)
        {
            SchoolId = schoolId;
            AcademicYearId = academicYearId;
        }
    }
}
