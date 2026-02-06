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
using YemenSchoolsV1.Application.Features.AcademicYears.Commands.CreateYear;
using YemenSchoolsV1.Application.Resources;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Features.Grades.Commands.Create
{
    public class CreateGradeCommandHandler : ResponseHandler, IRequestHandler<CreateGradeCommand, Response<string>>
    {
        #region faild
        private readonly IGradeRepository _gradeRepository;
        private readonly IMapper _mapper;

        public CreateGradeCommandHandler(IGradeRepository gradeRepository, IMapper mapper, IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            _gradeRepository = gradeRepository;
            _mapper = mapper;

        }

        #endregion

        public async Task<Response<string>> Handle(CreateGradeCommand request, CancellationToken cancellationToken)
        {
            var gradeDomain = _mapper.Map<Grade>(request);
            gradeDomain = await _gradeRepository.AddAsync(gradeDomain);
            if (gradeDomain == null)
            {
                return UnprocessableEntity<string>();
            }

            return Created(SharedResourcesKeys.Created);

        }


    }
}
