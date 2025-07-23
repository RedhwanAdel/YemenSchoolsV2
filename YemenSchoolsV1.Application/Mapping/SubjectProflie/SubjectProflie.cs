using AutoMapper;
using YemenSchoolsV1.Application.Features.Subjects.Queries.GetAll;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Mapping.SubjectProfile
{
    public partial class SubjectProfile : Profile
    {
        public SubjectProfile()
        {
            CreateMap<Subject, GetSubjectsListResponse>().ReverseMap();

        }
    }
}

