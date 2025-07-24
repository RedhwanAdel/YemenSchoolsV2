using AutoMapper;
using YemenSchoolsV1.Application.Features.Terms.Queries.GetByYearId;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Mapping.Terms
{
    public partial class TermsProfile : Profile
    {
        public TermsProfile()
        {
            CreateTermMapping();
            EditTermMapping();
            CreateMap<Term, GetTermByYearIdResponse>()
                    .ForMember(dest => dest.AcademicYearName, opt => opt.MapFrom(src => src.AcademicYear.Name))
                    .ReverseMap();

        }
    }
}
