using AutoMapper;
using YemenSchoolsV1.Application.Features.Accounts.Login;
using YemenSchoolsV1.Application.Features.Accounts.Register;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Mapping
{
	public partial class Accounts : Profile
	{
		public Accounts()
		{
			CreateMap<AppUser, RegisterCommand>()
				.ForMember(des => des.FullName, opt => opt.MapFrom(src => src.UserName))
				.ReverseMap();
			CreateMap<AppUser, LoginCommand>().ReverseMap();
		}
	}
}
