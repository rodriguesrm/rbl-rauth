using FluentValidation;
using RBlazeLabs.Common.Abstractions;
using MD = RBlazeLabs.Common.Validators.MessageValidationsDefault;

namespace RBlazeLabs.Common.Validators
{

    /// <summary>
    /// Enum cast from integer validator
    /// </summary>
    public class EnumCastFromIntegerValidator<TEnum> : AbstractValidator<int?>
        where TEnum : struct
    {

        /// <summary>
        /// Create a new instance of object
        /// </summary>
        /// <param name="fieldName">Field name</param>
        /// <param name="isRequired">Required status flag</param>
        public EnumCastFromIntegerValidator(string fieldName, bool isRequired)
        {

            string requiredMessage =
                ServiceActivator.GetStringInLocalizer<EnumCastFromIntegerValidator<TEnum>>(
                    "FIELD_REQUIRED", MD.FIELD_REQUIRED, fieldName);
            string invalidMessage =
                ServiceActivator.GetStringInLocalizer<EnumCastFromIntegerValidator<TEnum>>(
                    "INVALID_MESSAGE", MD.INVALID_MESSAGE, fieldName);

            RuleFor(e => e)
                .NotEmpty().WithMessage(requiredMessage)
                .Custom((e, c) =>
                {
                    if (e != null && !Enum.IsDefined(typeof(TEnum), e.GetType()))
                        c.AddFailure(fieldName, invalidMessage);
                }).When(e => e.HasValue)
            ;
            
        }

    }
}
