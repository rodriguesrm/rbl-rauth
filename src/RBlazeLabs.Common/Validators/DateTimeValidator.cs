using FluentValidation;

namespace RBlazeLabs.Common.Validators
{

    /// <summary>
    /// DateTime validator. Validate if date is not null and valid;
    /// </summary>
    public class DateTimeValidator : AbstractValidator<DateTime?>
    {

        /// <summary>
        /// Create a new instance of validator
        /// </summary>
        /// <param name="message">Critical message</param>
        public DateTimeValidator(string message)
        {

            RuleFor(d => d)
                .NotNull()
                .WithMessage(message)
            ;
            
        }

    }
}
