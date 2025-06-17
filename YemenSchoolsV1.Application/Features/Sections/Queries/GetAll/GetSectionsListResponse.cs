using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YemenSchoolsV1.Application.Features.Sections.Queries.GetAll
{
    public class GetSectionsListResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid GradeId { get; set; }

        public int? RoomNumber { get; set; }
    }
}
