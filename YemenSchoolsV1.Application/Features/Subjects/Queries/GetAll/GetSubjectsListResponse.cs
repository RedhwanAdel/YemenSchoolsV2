using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YemenSchoolsV1.Application.Features.Subjects.Queries.GetAll
{
    public class GetSubjectsListResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }

}
