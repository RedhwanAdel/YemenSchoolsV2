using FluentValidation;

namespace YemenSchoolsV1.Application.Features.Students.Commands.AddParentToStudent
{
    public class AddParentToStudentCommandValidator : AbstractValidator<AddParentToStudentCommand>
    {
        public AddParentToStudentCommandValidator()
        {
            RuleFor(x => x.StudentId)
                .NotEmpty().WithMessage("Student Id is required.");
            
            RuleFor(x => x.ParentId)
                .NotEmpty().WithMessage("Parent Id is required.");

            RuleFor(x => x.RelationType)
                .NotEmpty().WithMessage("Relation Type is required.");
        }
    }
}
