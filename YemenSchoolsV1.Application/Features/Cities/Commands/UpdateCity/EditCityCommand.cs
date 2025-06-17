using FinalProject.Application.Bases;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YemenSchoolsV1.Application.Features.Cities.Commands.UpdateCity
{
    public class EditCityCommand:IRequest<Response<EditCityResponse>>
    {
      

        public Guid Id { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public string? Image { get; set; }
    }
}
