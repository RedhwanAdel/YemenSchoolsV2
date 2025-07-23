using FinalProject.Application.Bases;
using MediatR;

namespace YemenSchoolsV1.Application.Features.AcademicYears.Queries.GetYears
{
    public class GetYearListQueary : IRequest<Response<List<GetYearListResponse>>>
    {

    }
}
