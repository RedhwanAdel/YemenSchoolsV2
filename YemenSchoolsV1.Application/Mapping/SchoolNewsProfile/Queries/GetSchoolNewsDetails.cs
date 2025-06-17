using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YemenSchoolsV1.Application.Features.Schools.Queries.GetSchoolDetails;
using YemenSchoolsV1.Application.Features.SchoolsNews.Queries.GetSchoolNewsDetails;
using YemenSchoolsV1.Application.Features.SchoolsNews.Queries.GetSchoolNewsList;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Mapping.SchoolNewsProfile
{
    public partial class SchoolNewsProfile
    {
        public void GetSchoolNewsDetails()
        {
            CreateMap<SchoolNews, GetSchoolNewsDetailsRseponse>()
             .ReverseMap();
        }
    }
}
