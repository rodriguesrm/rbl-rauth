using FluentValidation;
using MD = RBlazeLabs.Common.Validators.MessageValidationsDefault;
using SV = RBlazeLabs.Common.Validators.SimpleStringValidator;

namespace RBlazeLabs.Common.Validators
{

    /// <summary>
    /// String validator
    /// </summary>
    public class SimpleStringValidator : AbstractValidator<string?>
    {

        #region Constructors

        /// <summary>
        /// Create a new instance of object
        /// </summary>
        /// <param name="fieldName">Field name</param>
        /// <param name="isRequired">Indicate if field is required</param>
        public SimpleStringValidator(string fieldName, bool isRequired)
            : this(fieldName, isRequired, null, null, null) { }

        /// <summary>
        /// Create a new instance of object
        /// </summary>
        /// <param name="fieldName">Field name</param>
        /// <param name="isRequired">Indicate if field is required</param>
        /// <param name="pattern">Acceptance pattern regular expression</param>
        public SimpleStringValidator(string fieldName, bool isRequired, string pattern)
            : this(fieldName, isRequired, pattern, null, null) { }

        /// <summary>
        /// Create a new instance of object
        /// </summary>
        /// <param name="fieldName">Field name</param>
        /// <param name="isRequired">Indicate if field is required</param>
        /// <param name="minLen">Indicate a mininum length expression</param>
        /// <param name="maxLen">Indicate a maximum length expression</param>
        public SimpleStringValidator
        (
            string fieldName,
            bool isRequired,
            int? minLen,
            int? maxLen
        ) : this(fieldName, isRequired, null, minLen, maxLen) { }

        /// <summary>
        /// Create a new instance of object
        /// </summary>
        /// <param name="fieldName">Field name</param>
        /// <param name="isRequired">Indicate if field is required</param>
        /// <param name="pattern">Acceptance pattern regular expression</param>
        /// <param name="minLen">Indicate a mininum length expression</param>
        /// <param name="maxLen">Indicate a maximum length expression</param>
        public SimpleStringValidator
        (
            string fieldName,
            bool isRequired,
            string? pattern,
            int? minLen,
            int? maxLen)
        {

            int minimumLen = minLen ?? 0;
            int maximumLen = maxLen ?? 0;

            RuleFor(str => str)

                .NotEmpty()
                    .When(_ => isRequired)
                    .WithMessage(this.GetMessage<SV>("FIELD_REQUIRED", MD.FIELD_REQUIRED, fieldName))

                .Matches(pattern)
                    .When(x => !string.IsNullOrWhiteSpace(pattern))
                    .WithMessage(this.GetMessage<SV>("FIELD_INVALID", MD.FIELD_INVALID, fieldName))

                .Length(minimumLen).When(x => minimumLen > 0 && maximumLen > 0 && minimumLen == maximumLen)
                    .WithMessage(this.GetMessage<SV>("FIELD_MIN_SIZE", MD.FIELD_MIN_SIZE, fieldName, minimumLen))

                .MinimumLength(minimumLen)
                    .When(x => !string.IsNullOrWhiteSpace(x) && minimumLen > 0)
                    .WithMessage(this.GetMessage<SV>("FIELD_MIN_SIZE", MD.FIELD_MIN_SIZE, fieldName, minimumLen))

                .MaximumLength(maximumLen)
                    .When(x => !string.IsNullOrWhiteSpace(x) && maximumLen > 0 && maximumLen >= minimumLen)
                    .WithMessage(this.GetMessage<SV>("FIELD_MAX_SIZE", MD.FIELD_MAX_SIZE, fieldName, maximumLen))

            ;
        }

        #endregion

    }
}
