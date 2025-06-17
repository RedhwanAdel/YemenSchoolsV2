using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YemenSchoolsV1.Application.Features.Regions.Commands.CreateRegion;
using YemenSchoolsV1.Application.Features.Schools.Commands.CreateSchool;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Mapping.SchoolProfile
{
    public partial class SchoolProfile
    {
        public void CreateSchoolMapping()
        {

            CreateMap<School, CreateSchoolCommand>()
                .ReverseMap();

            CreateMap<School, CreateSchoolResponse>()
             .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Localize(src.NameAr, src.NameEn)))
             .ReverseMap();
        }
    }
}
