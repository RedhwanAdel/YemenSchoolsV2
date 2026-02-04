using MediatR;
using Microsoft.EntityFrameworkCore;
using YemenSchoolsV1.Application.Contracts.Persistence;

namespace YemenSchoolsV1.Application.Features.Students.Queries.GetStudentProfileWithParents
{
    public class GetStudentProfileWithParentsQueryHandler : IRequestHandler<GetStudentProfileWithParentsQuery, StudentWithParentsDto>
    {
        private readonly IStudentRepository _studentRepository;

        public GetStudentProfileWithParentsQueryHandler(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<StudentWithParentsDto> Handle(GetStudentProfileWithParentsQuery request, CancellationToken cancellationToken)
        {
            var student = await _studentRepository.GetStudentByIdWithParentsAsync(request.StudentId);
            if (student == null)
            {
               throw new KeyNotFoundException($"Student with Id {request.StudentId} not found.");
            }

            return new StudentWithParentsDto
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
        }
    }
}
