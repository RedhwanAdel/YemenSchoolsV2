using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YemenSchoolsV1.Application.Features.Schools.Commands.CreateSchool;
using YemenSchoolsV1.Application.Features.SchoolsNews.Commands.CreateSchoolNews;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Mapping.SchoolNewsProfile
{
    public partial class SchoolNewsProfile
    {
        public void CreateSchoolNewsMapping()
        {
            

            CreateMap<SchoolNews, CreateSchoolNewsCommand>()
                .ReverseMap();

            CreateMap<SchoolNews, CreateSchoolNewsResponse>()
             .ReverseMap();
        }
    }
}
