using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YemenSchoolsV1.Application.Features.Subjects.Queries.GetById
{
    public class GetSubjectDetailsResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }

}
