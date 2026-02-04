using AutoMapper;

namespace YemenSchoolsV1.Application.Mapping.Stages
{
    public partial class StagesProfile : Profile
    {
        public StagesProfile()
        {
            CreateStageMapping();
            EditStageMapping();
            GetStagesListMapping();

        }
    }
}
