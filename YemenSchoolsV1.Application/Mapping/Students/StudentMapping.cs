using AutoMapper;
using YemenSchoolsV1.Application.Features.Students.Commands.Create;
using YemenSchoolsV1.Application.Features.Students.Queries.GetById;
using YemenSchoolsV1.Application.Features.Students.Queries.GetStudentsPaged;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Mapping.Students
{
	public partial class StudentMapping : Profile
	{
		public StudentMapping()
		{
			CreateMap<Student, StudentDto>().ReverseMap();
			CreateMap<Student, GetStudentByIdResponse>().ReverseMap();
			CreateMap<Student, CreateStudentCommand>().ReverseMap();
		}
	}
}
