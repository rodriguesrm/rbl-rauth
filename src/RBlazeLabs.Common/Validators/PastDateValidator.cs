using FluentValidation;
using MD = RBlazeLabs.Common.Validators.MessageValidationsDefault;
using DV = RBlazeLabs.Common.Validators.PastDateValidator;

namespace RBlazeLabs.Common.Validators
{

    /// <summary>
    /// Past date validatotr. Validate if date is in past.
    /// </summary>
    public class PastDateValidator : AbstractValidator<DateTime?>
    {

        /// <summary>
        /// Create a new instance of validator
        /// </summary>
        /// <param name="field">Field name</param>
        /// <param name="requiredMessage">Critical message when date is null</param>
        /// <param name="todayDate">Today date to reference</param>
        public PastDateValidator(string field, string requiredMessage, DateTime? todayDate = null)
        {

            todayDate ??= DateTime.UtcNow;

            RuleFor(d => d)
                .NotNull()
                    .WithMessage(requiredMessage)
                .LessThan(todayDate.Value)
                    .When(d => d.HasValue)
                    .WithMessage(this.GetMessage<DV>("PAST_INVALID_DATE", MD.PAST_INVALID_DATE, field))
            ;

        }

    }
}
