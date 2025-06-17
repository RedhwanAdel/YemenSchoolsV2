using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YemenSchoolsV1.Application.Features.Cities.Commands.UpdateCity;

namespace YemenSchoolsV1.Application.Features.Cities.Commands.CreateCity
{
    public class CreateCityResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Image { get; set; }
    }
}
