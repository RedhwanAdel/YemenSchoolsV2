using FluentValidation;

namespace YemenSchoolsV1.Application.Features.Marks.Commands.CreateMarks
{
    public class CreateMarksCommandValidator : AbstractValidator<CreateMarksCommand>
    {
        public CreateMarksCommandValidator()
        {
            RuleFor(x => x.TeacherId)
                .NotEmpty().WithMessage("Teacher ID is required.");

            RuleFor(x => x.SectionSubjectId)
                .NotEmpty().WithMessage("Section Subject ID is required.");

            RuleFor(x => x.AssessmentType)
                .NotEmpty().WithMessage("Assessment Type is required.")
                .MaximumLength(50).WithMessage("Assessment Type must not exceed 50 characters.");

            RuleFor(x => x.MaxScore)
                .GreaterThan(0).WithMessage("Max Score must be greater than 0.");

            RuleFor(x => x.StudentScores)
                .NotEmpty().WithMessage("At least one student score is required.");

            RuleForEach(x => x.StudentScores)
                .Must(score => score.Value >= 0)
                .WithMessage("Score cannot be negative.");
        }
    }
}
