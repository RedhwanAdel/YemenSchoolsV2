using AutoMapper;
using YemenSchoolsV1.Application.Features.SubjectGrades.Commands.Create;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Mapping.SubjectGrades
{
	public class SubjectGradeProfile : Profile
	{
		public SubjectGradeProfile()
		{
			CreateMap<SubjectGrade, CreateSubjectGradeCommand>().ReverseMap();
		}
	}
}
