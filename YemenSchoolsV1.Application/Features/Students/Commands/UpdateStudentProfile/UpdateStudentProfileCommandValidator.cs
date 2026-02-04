using FluentValidation;

namespace YemenSchoolsV1.Application.Features.Students.Commands.UpdateStudentProfile
{
    public class UpdateStudentProfileCommandValidator : AbstractValidator<UpdateStudentProfileCommand>
    {
        public UpdateStudentProfileCommandValidator()
        {
            RuleFor(x => x.StudentId)
                .NotEmpty().WithMessage("Student Id is required.");

            RuleFor(x => x.NameAr)
                .NotEmpty().WithMessage("Arabic Name is required.")
                .MaximumLength(100).WithMessage("Arabic Name must not exceed 100 characters.");

            RuleFor(x => x.NameEn)
                .NotEmpty().WithMessage("English Name is required.")
                .MaximumLength(100).WithMessage("English Name must not exceed 100 characters.");
        }
    }
}
