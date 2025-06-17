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

namespace YemenSchoolsV1.Application.Features.Regions.Commands.CreateRegion
{
    public class CreateRegionValidator : AbstractValidator<CreateRegionCommand>
    {
        #region Fields
        private readonly IRegionService regionService;
        private readonly IStringLocalizer<SharedResources> _localizer;
        #endregion

        #region Constructors
        public CreateRegionValidator(IRegionService regionService,
                                   IStringLocalizer<SharedResources> localizer)
        {
            this.regionService = regionService;
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


        }
        #endregion

    }
}
