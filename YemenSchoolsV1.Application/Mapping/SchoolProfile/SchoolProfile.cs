using AutoMapper;

namespace YemenSchoolsV1.Application.Mapping.SchoolProfile
{
    public partial class SchoolProfile : Profile
    {
        public SchoolProfile()
        {
            CreateSchoolMapping();
            GetSchoolPagenatedListMapping();
            GetSchoolDetailsMapping();
            CreateSchoolPhonsMapping();
            EditSchoolForAdminMapping();
        }
    }
}
