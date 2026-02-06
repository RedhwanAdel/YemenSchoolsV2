using System.Linq;
using MediatR;
using Microsoft.Extensions.Localization;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Resources;

namespace YemenSchoolsV1.Application.Features.Students.Queries.GetStudentProfileWithParents
{
    public class GetStudentProfileWithParentsQueryHandler : ResponseHandler, IRequestHandler<GetStudentProfileWithParentsQuery, Response<StudentWithParentsDto>>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;

        public GetStudentProfileWithParentsQueryHandler(IStudentRepository studentRepository, IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            _studentRepository = studentRepository;
            _stringLocalizer = stringLocalizer;
        }

        public async Task<Response<StudentWithParentsDto>> Handle(GetStudentProfileWithParentsQuery request, CancellationToken cancellationToken)
        {
            var student = await _studentRepository.GetStudentByIdWithParentsAsync(request.StudentId);
            if (student == null)
            {
               return NotFound<StudentWithParentsDto>("Student not found.");
            }

            var result = new StudentWithParentsDto
            {
                Id = student.Id,
                RegisterNo = student.RegisterNo,
                NameAr = student.NameAr,
                NameEn = student.NameEn,
                Nationality = student.Nationality,
                Address = student.Address,
                Gender = student.Gender,
                DateOfBirth = student.DateOfBirth,
                PhoneNumber = student.PhoneNumber,
                Email = student.Email,
                Parents = student.Parents.Select(ps => new ParentSummaryDto
                {
                    ParentId = ps.ParentId,
                    NameAr = ps.Parent.NameAr,
                    NameEn = ps.Parent.NameEn, 
                    RelationType = ps.RelationType
                }).ToList()
            };

            return Success(result);
        }
    }
}
