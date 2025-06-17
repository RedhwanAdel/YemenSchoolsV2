using YemenSchoolsV1.Application.Features.Teachers.Queries.GetAllBySchoolId;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Mapping.TeacherProfile
{
	public partial class TeacherProfile
	{
		public void GetAllBySchoolIdMapping()
		{
			CreateMap<Teacher, GetTeachersListResponse>()
			  .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Localize(src.NameAr, src.NameEn)))

			  .ReverseMap();
		}
	}
}