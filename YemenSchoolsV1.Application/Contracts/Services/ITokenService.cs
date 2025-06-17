using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Contracts.Services
{
	public interface ITokenService
	{
		Task<string> CreateToken(AppUser user);
	}
}
