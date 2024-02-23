using FluentValidation;
using MD = RBlazeLabs.Common.Validators.MessageValidationsDefault;
using DV = RBlazeLabs.Common.Validators.FutureDateValidator;

namespace RBlazeLabs.Common.Validators
{

    /// <summary>
    /// Contract future date validator. Validate date if is in future.
    /// </summary>
    public class FutureDateValidator : AbstractValidator<DateTime?>
    {

        /// <summary>
        /// Create a new instance of validator
        /// </summary>
        /// <param name="field">Field name</param>
        /// <param name="requiredMessage">Critical message when date is null</param>
        /// <param name="todayDate">Today date to reference</param>
        public FutureDateValidator(string field, string requiredMessage, DateTime? todayDate = null)
        {

            todayDate ??= DateTime.UtcNow;

            RuleFor(d => d)
                .NotNull()
                    .WithMessage(requiredMessage)
                .GreaterThan(todayDate.Value)
                    .When(d => d.HasValue)
                    .WithMessage(this.GetMessage<DV>("FUTURE_INVALID_DATE", MD.FUTURE_INVALID_DATE, field))
            ;

        }

    }
}
