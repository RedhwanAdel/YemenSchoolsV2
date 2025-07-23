using AutoMapper;

namespace YemenSchoolsV1.Application.Mapping.Terms
{
    public partial class TermsProfile : Profile
    {
        public TermsProfile()
        {
            CreateTermMapping();
            EditTermMapping();
            GetTermListMapping();

        }
    }
}
