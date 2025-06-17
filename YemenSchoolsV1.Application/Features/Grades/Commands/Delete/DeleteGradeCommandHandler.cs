using AutoMapper;
using FinalProject.Application.Bases;
using MediatR;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YemenSchoolsV1.Application.Contracts.Services;
using YemenSchoolsV1.Application.Features.AcademicYears.Commands.DeleteYear;
using YemenSchoolsV1.Application.Features.Grades.Commands.Create;
using YemenSchoolsV1.Application.Features.Grades.Commands.Update;
using YemenSchoolsV1.Application.Features.Terms.Commands.DeleteTerm;
using YemenSchoolsV1.Application.Resources;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Features.Grades.Commands.Delete
{
    public class DeleteGradeCommandHandler : ResponseHandler, IRequestHandler<DeleteTermCommand, Response<bool>>
    {
        #region faild
        private readonly IGradeService gradeService;
        private readonly IStringLocalizer<SharedResources> stringLocalizer;
        #endregion

        #region ctor
        public DeleteGradeCommandHandler(IGradeService gradeService, IMapper mapper, IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            this.gradeService = gradeService;
            this.stringLocalizer = stringLocalizer;

        }


        #endregion
        public async Task<Response<bool>> Handle(DeleteTermCommand request, CancellationToken cancellationToken)
        {
            var grade = await gradeService.DeleteGradeAsync(request.Id);
            if (grade is false)
            {
                return UnprocessableEntity<bool>();
            }
            return Deleted<bool>();
        }




    }
}
