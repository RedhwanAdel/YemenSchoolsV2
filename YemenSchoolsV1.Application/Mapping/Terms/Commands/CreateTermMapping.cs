using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YemenSchoolsV1.Application.Features.AcademicYears.Commands.CreateYear;
using YemenSchoolsV1.Application.Features.Terms.Commands.CreateTerm;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Mapping.Terms
{
    public partial class TermsProfile
    {
        public void CreateTermMapping()
        {
            CreateMap<Term, CreateTermCommand>().ReverseMap();

        }
    }
}
