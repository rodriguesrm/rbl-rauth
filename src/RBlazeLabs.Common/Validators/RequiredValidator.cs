using FluentValidation;

namespace RBlazeLabs.Common.Validators
{

    /// <summary>
    /// Validator for required fields
    /// </summary>
    public class RequiredValidator : AbstractValidator<object>
    {

        /// <summary>
        /// Create a new instance of validator
        /// </summary>
        /// <param name="message">Critical message</param>
        public RequiredValidator(string message)
        {

            RuleFor(o => o)
                .NotNull()
                    .WithMessage(message)
                .NotEmpty()
                    .When(o => o?.GetType() == typeof(string))
                    .WithMessage(message)
            ;

        }

    }
}
