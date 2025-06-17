using FinalProject.Application.Bases;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YemenSchoolsV1.Application.Features.Cities.Commands.DeleteCity
{
    public class DeleteCityCommand:IRequest<Response<bool>>
    {
        public DeleteCityCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
