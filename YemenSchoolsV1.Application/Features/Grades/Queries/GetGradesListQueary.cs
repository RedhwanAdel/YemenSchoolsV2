using YemenSchoolsV1.Application.Bases;
using MediatR;

namespace YemenSchoolsV1.Application.Features.Grades.Queries
{
    public class GetGradesListQueary : IRequest<Response<List<GetGradesListResponse>>>
    {

    }
}
