using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YemenSchoolsV1.Domain.Entities;
using YemenSchoolsV1.Domain.Enums;

namespace YemenSchoolsV1.Application.Features.Schools.Queries.GetSchoolsPaginated
{
    public class GetSchoolPagenatedListResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Logo { get; set; }
        public string SchoolType { get; set; } 
        public string GenderType { get; set; } 
        public string City { get; set; } 
        public string Region { get; set; } 
      //  public double Rating { get; set; } 

      
    }
}
