using FinalProject.Application.Bases;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YemenSchoolsV1.Application.Features.Cities.Queries.GetCityDetails
{
    public class GetCityDetailsQueary:IRequest<Response<GetCityDetailsResponse>>
    {
        public GetCityDetailsQueary(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; set; }
    }
}
