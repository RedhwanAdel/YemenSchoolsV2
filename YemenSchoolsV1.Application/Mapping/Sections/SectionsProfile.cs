using AutoMapper;
using YemenSchoolsV1.Application.Features.Sections.Queries.GetById;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Mapping.Sections
{
    public partial class SectionsProfile : Profile
    {
        public SectionsProfile()
        {
            CreateSectionMapping();
            EditSectionMapping();
            GetSectionsListMapping();
            CreateMap<Section, GetSectionByIdResponse>().ReverseMap();

        }
    }
}
