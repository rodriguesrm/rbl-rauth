using FluentValidation;
using MD = RBlazeLabs.Common.Validators.MessageValidationsDefault;
using DV = RBlazeLabs.Common.Validators.PeriodDateValidator;

namespace RBlazeLabs.Common.Validators
{

    /// <summary>
    /// Period date validator. Validate if date is between in period.
    /// </summary>
    public class PeriodDateValidator : AbstractValidator<DateTime?>
    {

        /// <summary>
        /// Create a new instance of validator
        /// </summary>
        /// <param name="startDate">Start date to validate</param>
        /// <param name="endDate">End date to validate</param>
        /// <param name="field">Field name</param>
        public PeriodDateValidator(DateTime? startDate, DateTime? endDate, string field)
        {

            RuleFor(d => d)
                .Must(d => startDate.HasValue)
                    .WithMessage(this.GetMessage<DV>("START_DATE_INVALID", MD.START_DATE_INVALID, field))
                .Must(d => endDate.HasValue)
                    .WithMessage(this.GetMessage<DV>("END_DATE_INVALID", MD.END_DATE_INVALID, field))
                .Must(d => endDate >= startDate)
                    .When(d => startDate.HasValue && endDate.HasValue)
                    .WithMessage(this.GetMessage<DV>("DATES_INVALID", MD.DATES_INVALID))
            ;

        }

    }
}
