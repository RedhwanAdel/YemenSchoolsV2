using AutoMapper;

namespace YemenSchoolsV1.Application.Mapping.TeacherProfile
{
	public partial class TeacherProfile : Profile
	{
		public TeacherProfile()
		{
			CreateTeacherMapping();
			GetTeacherDetailsMapping();
			EditTeacherMapping();
			GetAllBySchoolIdMapping();
		}
	}
}