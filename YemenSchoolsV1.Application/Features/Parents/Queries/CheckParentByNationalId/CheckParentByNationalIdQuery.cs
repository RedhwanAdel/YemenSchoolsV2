using MediatR;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Dto.Parents;

namespace YemenSchoolsV1.Application.Features.Parents.Queries.CheckParentByNationalId
{
    public class CheckParentByNationalIdQuery : IRequest<Response<ParentCheckDto>>
    {
        public string NationalId { get; set; }

        public CheckParentByNationalIdQuery(string nationalId)
        {
            NationalId = nationalId;
        }
    }
}
