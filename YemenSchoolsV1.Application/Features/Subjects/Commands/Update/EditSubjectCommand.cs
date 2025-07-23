using FinalProject.Application.Bases;
using MediatR;

namespace YemenSchoolsV1.Application.Features.Subjects.Commands.Update
{
    public class EditSubjectCommand : IRequest<Response<string>>
    {
        public Guid Id { get; set; }

        public string? NameAr { get; set; }
        public string? NameEn { get; set; }
    }
}
