using FinalProject.Application.Bases;
using MediatR;

namespace YemenSchoolsV1.Application.Features.Cities.Queries.GetCities
{
    public class GetCitiesListQueary:IRequest<Response<List<GetCitiesListResponse>>>
    {
    }
}
