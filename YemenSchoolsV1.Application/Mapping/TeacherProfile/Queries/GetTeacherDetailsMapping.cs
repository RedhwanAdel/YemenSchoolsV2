using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YemenSchoolsV1.Application.Features.Teachers.Queries.GetById;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Mapping.TeacherProfile
{
    public partial class TeacherProfile
    {
        public void GetTeacherDetailsMapping()
        {
            CreateMap<Teacher, GetTeacherDetailsResponse>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Localize(src.NameAr, src.NameEn)))

                .ReverseMap();
        }
    }
}
