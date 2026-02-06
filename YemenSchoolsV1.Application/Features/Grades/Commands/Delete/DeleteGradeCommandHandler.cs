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
using YemenSchoolsV1.Application.Resources;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Features.Grades.Commands.Delete
{
    public class DeleteGradeCommandHandler : ResponseHandler, IRequestHandler<DeleteGradeCommand, Response<bool>>
    {
        private readonly IGradeRepository _gradeRepository;

        public DeleteGradeCommandHandler(IGradeRepository gradeRepository, IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            _gradeRepository = gradeRepository;

        }



        public async Task<Response<bool>> Handle(DeleteGradeCommand request, CancellationToken cancellationToken)
        {
            var grade = await _gradeRepository.GetByIdAsync(request.Id);
            if (grade == null)
            {
                return NotFound<bool>();
            }

            var deleted = await _gradeRepository.DeleteAsync(request.Id);
            if (!deleted)
            {
                return UnprocessableEntity<bool>();
            }
            return Deleted<bool>();
        }




    }
}
