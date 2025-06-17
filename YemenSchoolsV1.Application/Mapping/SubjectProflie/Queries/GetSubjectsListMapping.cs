using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YemenSchoolsV1.Application.Features.Subjects.Queries.GetAll;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Mapping.SubjectProfile
{
    public partial class SubjectProfile
    {
        public void GetSubjectsListMapping()
        {
            CreateMap<Subject, GetSubjectsListResponse>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Localize(src.NameAr, src.NameEn)))

                .ReverseMap();
        }
    }
}

