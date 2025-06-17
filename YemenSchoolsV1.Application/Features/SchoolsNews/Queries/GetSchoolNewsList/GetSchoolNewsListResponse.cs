using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YemenSchoolsV1.Application.Features.SchoolsNews.Queries.GetSchoolNewsList
{
    public class GetSchoolNewsListResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; } // عنوان التحديث
        public string? MainPhoto { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
