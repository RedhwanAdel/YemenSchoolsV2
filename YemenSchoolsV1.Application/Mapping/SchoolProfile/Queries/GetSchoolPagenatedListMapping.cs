using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YemenSchoolsV1.Application.Features.Schools.Queries.GetSchoolsPaginated;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Mapping.SchoolProfile
{
    public partial class SchoolProfile
    {
        public void GetSchoolPagenatedListMapping()
        {
            CreateMap<School, GetSchoolPagenatedListResponse>()
              .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Localize(src.NameAr, src.NameEn)))

              .ForMember(dest => dest.SchoolType, opt => opt.MapFrom(src => src.SchoolType.ToString()))
              .ForMember(dest => dest.GenderType, opt => opt.MapFrom(src => src.GenderType.ToString()))
              .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Localize(src.City.NameAr, src.City.NameEn)))
              .ForMember(dest => dest.Region, opt => opt.MapFrom(src => src.Localize(src.Region.NameAr, src.Region.NameEn))).ReverseMap();
        }




    }
}
