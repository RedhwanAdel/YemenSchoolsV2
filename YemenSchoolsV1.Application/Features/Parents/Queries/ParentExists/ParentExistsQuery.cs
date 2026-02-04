using MediatR;
using YemenSchoolsV1.Application.Bases;

namespace YemenSchoolsV1.Application.Features.Parents.Queries.ParentExists
{
    public class ParentExistsQuery : IRequest<Response<bool>>
    {
        public string NationalId { get; set; }

        public ParentExistsQuery(string nationalId)
        {
            NationalId = nationalId;
        }
    }
}
