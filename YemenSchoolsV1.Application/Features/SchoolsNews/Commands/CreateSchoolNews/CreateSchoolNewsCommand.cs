using FinalProject.Application.Bases;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YemenSchoolsV1.Application.Features.Schools.Commands.CreateSchool;

namespace YemenSchoolsV1.Application.Features.SchoolsNews.Commands.CreateSchoolNews
{
    public class CreateSchoolNewsCommand : IRequest<Response<CreateSchoolNewsResponse>>
    {
        public Guid SchoolId { get; set; }
        public string Title { get; set; } // عنوان التحديث
        public string Description { get; set; } // وصف التحديث
        public string? MainPhoto { get; set; }
    }
}
