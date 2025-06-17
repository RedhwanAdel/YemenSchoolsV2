using FinalProject.Application.Bases;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YemenSchoolsV1.Application.Features.Regions.Queries.GetRegionDetails
{
    public class GetRegionDetailsQueary:IRequest<Response<GetRegionDetailsResponse>>
    {
        public GetRegionDetailsQueary(Guid id)
        {
            Id= id;
        }
        public Guid Id { get; set; }
    }
}
