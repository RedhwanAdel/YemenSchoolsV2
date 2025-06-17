using FluentValidation;
using Microsoft.Extensions.Localization;
using YemenSchoolsV1.Application.Contracts.Services;
using YemenSchoolsV1.Application.Resources;

namespace YemenSchoolsV1.Application.Features.Cities.Commands.CreateCity
{
    public class CreateCityValidator: AbstractValidator<CreateCityCommand>
    {



        #region Fields
        private readonly ICityService _cityService;
        private readonly IStringLocalizer<SharedResources> _localizer;
        #endregion

        #region Constructors
        public CreateCityValidator(ICityService cityService,
                                   IStringLocalizer<SharedResources> localizer)
        {
            _cityService = cityService;
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
