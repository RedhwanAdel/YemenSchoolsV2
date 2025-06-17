using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YemenSchoolsV1.Application.Features.SchoolsNews.Queries.GetSchoolNewsDetails
{
    public class GetSchoolNewsDetailsRseponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; } 
        public string Description { get; set; }
        public string? MainPhoto { get; set; }
        public DateTime CreatedDate { get; set; } 
    }
}
