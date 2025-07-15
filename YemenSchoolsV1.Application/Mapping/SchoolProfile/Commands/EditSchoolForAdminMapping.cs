using YemenSchoolsV1.Application.Features.Schools.Commands.UpdateSchool;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Mapping.SchoolProfile
{
    public partial class SchoolProfile
    {
        public void EditSchoolForAdminMapping()
        {

            CreateMap<School, EditSchoolForAdminCommand>()
                .ReverseMap();

        }
    }
}
