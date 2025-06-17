using FinalProject.Application.Bases;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YemenSchoolsV1.Application.Features.Subjects.Queries.GetAll
{
    public class GetSubjectsListQuery : IRequest<Response<List<GetSubjectsListResponse>>>
    {
    }
}
