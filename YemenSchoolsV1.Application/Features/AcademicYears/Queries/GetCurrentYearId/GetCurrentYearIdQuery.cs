using MediatR;
using YemenSchoolsV1.Application.Bases;

namespace YemenSchoolsV1.Application.Features.AcademicYears.Queries.GetCurrentYearId
{
    public class GetCurrentYearIdQuery : IRequest<Response<Guid>>
    {
        public Guid SchoolId { get; set; }

        public GetCurrentYearIdQuery(Guid schoolId)
        {
            SchoolId = schoolId;
        }
    }
}
