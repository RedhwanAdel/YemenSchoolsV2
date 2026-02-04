using FluentValidation;

namespace YemenSchoolsV1.Application.Features.Marks.Commands.UpdateMarks
{
    public class UpdateMarksCommandValidator : AbstractValidator<UpdateMarksCommand>
    {
        public UpdateMarksCommandValidator()
        {
            RuleFor(x => x.TeacherId)
                .NotEmpty().WithMessage("Teacher ID is required.");

            RuleFor(x => x.SectionSubjectId)
                .NotEmpty().WithMessage("Section Subject ID is required.");

            RuleFor(x => x.AssessmentType)
                .NotEmpty().WithMessage("Assessment Type is required.");

            RuleFor(x => x.StudentScores)
                .NotEmpty().WithMessage("At least one student score is required.");

            RuleForEach(x => x.StudentScores)
                .Must(score => score.Value >= 0)
                .WithMessage("Score cannot be negative.");
        }
    }
}
