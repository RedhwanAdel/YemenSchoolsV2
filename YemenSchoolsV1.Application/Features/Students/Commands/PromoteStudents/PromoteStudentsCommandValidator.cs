using FluentValidation;

namespace YemenSchoolsV1.Application.Features.Students.Commands.PromoteStudents
{
    public class PromoteStudentsCommandValidator : AbstractValidator<PromoteStudentsCommand>
    {
        public PromoteStudentsCommandValidator()
        {
            RuleFor(x => x.StudentIds)
                .NotEmpty().WithMessage("At least one student must be selected for promotion.");

            RuleFor(x => x.NewSectionId)
                .NotEmpty().WithMessage("New Section Id is required.");
        }
    }
}
