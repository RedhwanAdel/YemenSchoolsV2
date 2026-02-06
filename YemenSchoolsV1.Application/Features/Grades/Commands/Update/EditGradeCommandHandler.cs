using AutoMapper;
using YemenSchoolsV1.Application.Bases;
using MediatR;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Features.AcademicYears.Commands.UpdateYear;
using YemenSchoolsV1.Application.Features.Grades.Commands.Create;
using YemenSchoolsV1.Application.Resources;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Features.Grades.Commands.Update
{
    public class EditGradeCommandHandler : ResponseHandler, IRequestHandler<EditGradeCommand, Response<string>>
    {

        #region faild
        private readonly IGradeRepository _gradeRepository;
        private readonly IMapper _mapper;

        public EditGradeCommandHandler(IGradeRepository gradeRepository, IMapper mapper, IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            _gradeRepository = gradeRepository;
            _mapper = mapper;

        }

        #endregion
        public async Task<Response<string>> Handle(EditGradeCommand request, CancellationToken cancellationToken)
        {
            if (request == null || request.Id == Guid.Empty)
            {
                return BadRequest<string>();
            }

            var gradeDomain = _mapper.Map<Grade>(request);
            gradeDomain = await _gradeRepository.UpdateAsync(request.Id, gradeDomain);
            if (gradeDomain == null)
            {
                return UnprocessableEntity<string>();
            }

            return Success<string>(SharedResourcesKeys.Update);
        }

    }
}
