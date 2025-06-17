using FluentValidation;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YemenSchoolsV1.Application.Contracts.Services;
using YemenSchoolsV1.Application.Features.Cities.Commands.CreateCity;
using YemenSchoolsV1.Application.Resources;

namespace YemenSchoolsV1.Application.Features.Schools.Commands.CreateSchool
{
    public class CreateSchoolValidator : AbstractValidator<CreateSchoolCommand>
    {
        #region Fields
        private readonly ISchoolService schoolService;
        private readonly IStringLocalizer<SharedResources> _localizer;
        #endregion

        #region Constructors
        public CreateSchoolValidator(ISchoolService schoolService,
                                   IStringLocalizer<SharedResources> localizer)
        {
            this.schoolService = schoolService;
            _localizer = localizer;
            ApplyValidationsRules();

        }
        #endregion

        #region Actions
        public void ApplyValidationsRules()
        {
            RuleFor(x => x.NameAr)
                 .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                 .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required])
                 .MinimumLength(3).WithMessage(_localizer[SharedResourcesKeys.MinLengthis3])
                 .MaximumLength(100).WithMessage(_localizer[SharedResourcesKeys.MaxLengthis100]);

            RuleFor(x => x.NameEn)
                   .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                   .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required])
                   .MinimumLength(3).WithMessage(_localizer[SharedResourcesKeys.MinLengthis3])
                   .MaximumLength(100).WithMessage(_localizer[SharedResourcesKeys.MaxLengthis100]);


            RuleFor(x => x.AddressAr)
                .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required])
                .MinimumLength(3).WithMessage(_localizer[SharedResourcesKeys.MinLengthis3])
                .MaximumLength(100).WithMessage(_localizer[SharedResourcesKeys.MaxLengthis100]);

            RuleFor(x => x.AddressEn)
                   .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                   .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required])
                   .MinimumLength(3).WithMessage(_localizer[SharedResourcesKeys.MinLengthis3])
                   .MaximumLength(100).WithMessage(_localizer[SharedResourcesKeys.MaxLengthis100]);




            RuleFor(x => x.PostalCode)
           .Matches("^[0-9]{5}$").When(x => !string.IsNullOrEmpty(x.PostalCode))
           .WithMessage(_localizer[SharedResourcesKeys.mustBe5No]);

            RuleFor(x => x.MainPhone)
                .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.Required])
                .MinimumLength(10).WithMessage(_localizer[SharedResourcesKeys.Atlest6No])
                .MaximumLength(15).WithMessage(_localizer[SharedResourcesKeys.AtMostNo12No]);

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.Required])
                .EmailAddress().WithMessage(_localizer[SharedResourcesKeys.EmilNotVaild]);

            RuleFor(x => x.SchoolType)
                .IsInEnum().WithMessage(_localizer[SharedResourcesKeys.NotValid]);

            RuleFor(x => x.CityId)
                .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.Required]);

            RuleFor(x => x.RegionId)
                .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.Required]);

           
            RuleFor(x => x.GenderType)
                .IsInEnum().WithMessage(_localizer[SharedResourcesKeys.NotValid]);

            RuleFor(x => x.CurriculumType)
                .IsInEnum().WithMessage(_localizer[SharedResourcesKeys.NotValid]);

            RuleFor(x => x.SchoolLevel)
                .IsInEnum().WithMessage(_localizer[SharedResourcesKeys.NotValid]);
        }



        #endregion
    }
}
