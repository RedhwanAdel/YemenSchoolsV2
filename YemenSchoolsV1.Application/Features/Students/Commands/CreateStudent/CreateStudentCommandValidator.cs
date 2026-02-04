using FluentValidation;

namespace YemenSchoolsV1.Application.Features.Students.Commands.CreateStudent
{
    public class CreateStudentCommandValidator : AbstractValidator<CreateStudentCommand>
    {
        public CreateStudentCommandValidator()
        {
            RuleFor(x => x.NameAr)
                .NotEmpty().WithMessage("Arabic Name is required.")
                .MaximumLength(100).WithMessage("Arabic Name must not exceed 100 characters.");

            RuleFor(x => x.NameEn)
                .NotEmpty().WithMessage("English Name is required.")
                .MaximumLength(100).WithMessage("English Name must not exceed 100 characters.");

            RuleFor(x => x.RegisterNo)
                .NotEmpty().WithMessage("Register No is required.");

            RuleFor(x => x.SchoolId)
                .NotEmpty().WithMessage("School Id is required.");
                
            RuleFor(x => x.CurrentAcademicYearId)
                .NotEmpty().WithMessage("Academic Year Id is required.");

            RuleFor(x => x.CurrentSectionId)
                .NotEmpty().WithMessage("Section Id is required.");
        }
    }
}
