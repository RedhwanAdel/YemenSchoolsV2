using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YemenSchoolsV1.Application.Features.Schools.Queries.GetSchoolDetails;
using YemenSchoolsV1.Application.Features.Schools.Queries.GetSchoolsPaginated;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Mapping.SchoolProfile
{
    public partial class SchoolProfile
    {
        public void GetSchoolDetailsMapping()
        {
            CreateMap<School, GetSchoolDetailsResponse>()
             .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Localize(src.NameAr, src.NameEn)))
             .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Localize(src.DescriptionAr, src.DescriptionEn)))
             .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Localize(src.AddressAr, src.AddressEn)))

             .ForMember(dest => dest.PhoneNumberList, opt => opt.MapFrom(src => src.SchoolPhones))
             .ForMember(dest => dest.CurriculumType, opt => opt.MapFrom(src => src.CurriculumType.ToString()))
             .ForMember(dest => dest.SchoolLevel, opt => opt.MapFrom(src => src.SchoolLevel.ToString()))
             .ForMember(dest => dest.SchoolType, opt => opt.MapFrom(src => src.SchoolType.ToString()))
             .ForMember(dest => dest.GenderType, opt => opt.MapFrom(src => src.GenderType.ToString()))
             .ForMember(dest => dest.CityName, opt => opt.MapFrom(src => src.Localize(src.City.NameAr, src.City.NameEn)))
             .ForMember(dest => dest.RegionName, opt => opt.MapFrom(src => src.Localize(src.Region.NameAr, src.Region.NameEn))).ReverseMap();

            CreateMap<SchoolPhone, PhoneResponse>();


        }
    }
}
