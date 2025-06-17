using FinalProject.Application.Bases;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YemenSchoolsV1.Application.Features.Regions.Queries.GetRegions;

namespace YemenSchoolsV1.Application.Features.SchoolsNews.Queries.GetSchoolNewsList
{
    public class GetSchoolNewsListQuerey : IRequest<Response<List<GetSchoolNewsListResponse>>>
    {
        public GetSchoolNewsListQuerey(Guid schoolId)
        {
            SchoolId = schoolId;
        }
        public Guid SchoolId { get; set; }
    }
}
