using MediatR;
using YemenSchoolsV1.Application.Bases;

namespace YemenSchoolsV1.Application.Features.Accounts.Commands.SessionLogin
{
    public class SessionLoginCommand : IRequest<Response<string>>
    {
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public SessionLoginCommand(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }
    }
}
