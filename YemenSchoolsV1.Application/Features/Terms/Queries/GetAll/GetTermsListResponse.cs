using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YemenSchoolsV1.Application.Features.Terms.Queries.GetAll
{
    public class GetTermsListResponse
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        public Guid AcademicYearId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
