using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YemenSchoolsV1.Application.Features.SchoolsNews.Commands.CreateSchoolNews
{
    public class CreateSchoolNewsResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; } // عنوان التحديث
    }
}
