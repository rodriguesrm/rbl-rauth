using FluentValidation;
using MD = RBlazeLabs.Common.Validators.MessageValidationsDefault;
using FV = RBlazeLabs.Common.Validators.FullNameValidator;
using RBlazeLabs.Common.Contracts.Entities;

namespace RBlazeLabs.Common.Validators
{

    /// <summary>
    /// Full name validator
    /// </summary>
    public class FullNameValidator : AbstractValidator<IFullName>
    {

        #region Construtors

        public FullNameValidator() : this(new FullNameValidatorArgs()
        {
            CharListAllowed = string.Empty,
            FirstNameMinimumLength = 2,
            FirstNameMaximumLength = 50,
            LastNameMinimumLength = 2,
            LastNameMaximumLength = 100
        }) { }

        /// <summary>
        /// Create a new instance of object
        /// </summary>
        /// <param name="args">Full name validation arguments</param>
        public FullNameValidator(FullNameValidatorArgs args)
        {

            // Regular expression for all characteres name (global)
            // ^[a-zA-ZàáâäãåąčćęèéêëėįìíîïłńòóôöõøùúûüųūÿýżźñçčšžÀÁÂÄÃÅĄĆČĖĘÈÉÊËÌÍÎÏĮŁŃÒÓÔÖÕØÙÚÛÜŲŪŸÝŻŹÑßÇŒÆČŠŽ∂ð ,.'-]+$

            RuleFor(n => n.FirstName)
                .NotEmpty()
                    .WithMessage(this.GetMessage<FV>("FIRST_NAME_REQUIRED", MD.FIRST_NAME_REQUIRED))
                .MinimumLength(args.FirstNameMinimumLength)
                    .WithMessage(this.GetMessage<FV>("FIRST_NAME_MIN_SIZE", MD.FIRST_NAME_MIN_SIZE))
                .MaximumLength(args.FirstNameMaximumLength)
                    .WithMessage(this.GetMessage<FV>("FIRST_NAME_MAX_SIZE", MD.FIRST_NAME_MAX_SIZE))
                .Matches($"^[a-zA-Z{args.CharListAllowed} ,.'-]+$")
                    .WithMessage(this.GetMessage<FV>("FIRST_NAME_INVALID_CHARS", MD.FIRST_NAME_INVALID_CHARS))
            ;

            RuleFor(n => n.LastName)
                .NotEmpty()
                    .WithMessage(this.GetMessage<FV>("LAST_NAME_REQUIRED", MD.LAST_NAME_REQUIRED))
                .MinimumLength(args.LastNameMinimumLength)
                    .WithMessage(this.GetMessage<FV>("LAST_NAME_MIN_SIZE", MD.LAST_NAME_MIN_SIZE))
                .MaximumLength(args.LastNameMaximumLength)
                    .WithMessage(this.GetMessage<FV>("LAST_NAME_MAX_SIZE", MD.LAST_NAME_MAX_SIZE))
                .Matches($"^[a-zA-Z{args.CharListAllowed} ,.'-]+$")
                    .WithMessage(this.GetMessage<FV>("LAST_NAME_INVALID_CHARS", MD.LAST_NAME_INVALID_CHARS))
            ;

        }

        #endregion

    }
}
