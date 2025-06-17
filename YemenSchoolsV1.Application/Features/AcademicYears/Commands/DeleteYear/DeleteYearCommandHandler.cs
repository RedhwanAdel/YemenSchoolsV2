using FinalProject.Application.Bases;
using MediatR;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YemenSchoolsV1.Application.Contracts.Services;
using YemenSchoolsV1.Application.Features.Cities.Commands.DeleteCity;
using YemenSchoolsV1.Application.Resources;

namespace YemenSchoolsV1.Application.Features.AcademicYears.Commands.DeleteYear
{
    public class DeleteYearCommandHandler : ResponseHandler, IRequestHandler<DeleteYearCommand, Response<bool>>
    {
        private readonly IAcademicYearService academicYearService;

        public DeleteYearCommandHandler(IStringLocalizer<SharedResources> stringLocalizer, IAcademicYearService academicYearService) : base(stringLocalizer)
        {
            this.academicYearService = academicYearService;
        }

       

        public async Task<Response<bool>> Handle(DeleteYearCommand request, CancellationToken cancellationToken)
        {
            var year = await academicYearService.DeleteYearAsync(request.Id);
            if (year is false)
            {
                return UnprocessableEntity<bool>();
            }
            return Deleted<bool>();
        }
    }
}
