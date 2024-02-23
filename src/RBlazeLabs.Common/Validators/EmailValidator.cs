using FluentValidation;
using MD = RBlazeLabs.Common.Validators.MessageValidationsDefault;
using EV = RBlazeLabs.Common.Validators.EmailValidator;
using RBlazeLabs.Common.ValueObjects;

namespace RBlazeLabs.Common.Validators
{

    /// <summary>
    /// E-mail validator
    /// </summary>
    public class EmailValidator : AbstractValidator<Email>
    {

        /// <summary>
        /// Craate a new instance of validator
        /// </summary>
        /// <param name="isRequired">Indicates whether email is required (default true)</param>
        public EmailValidator(bool isRequired = true)
        {

            RuleFor(e => e.Address)

                .NotEmpty().When(e => isRequired)
                    .WithMessage(this.GetMessage<EV>("EMAIL_REQUIRED", MD.FIELD_REQUIRED, "E-mail"))
                .EmailAddress()
                    .WithMessage(this.GetMessage<EV>("EMAIL_INVALID", MD.EMAIL_INVALID))

            ;

        }

    }

}
