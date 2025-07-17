using AutoMapper;
using YemenSchoolsV1.Application.Features.Terms.Queries.GetById;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Mapping.Terms
{
    public partial class TermsProfile : Profile
    {
        public TermsProfile()
        {
            CreateTermMapping();
            EditTermMapping();
            GetTermListMapping();
            CreateMap<Term, GetTermByIdResponse>().ReverseMap();

        }
    }
}
