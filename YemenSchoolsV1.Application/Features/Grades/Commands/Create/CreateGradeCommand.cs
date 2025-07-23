using FinalProject.Application.Bases;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace YemenSchoolsV1.Application.Features.Grades.Commands.Create
{
    public class CreateGradeCommand : IRequest<Response<string>>
    {
        [Required]
        public string Name { get; set; } = string.Empty;
    }
}
