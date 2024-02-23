using FluentValidation;
using Doc = RSoft.Helpers.Validations.BrasilianDocument;
using MD = RBlazeLabs.Common.Validators.MessageValidationsDefault;
using DV = RBlazeLabs.Common.Validators.BrCpfCnpjValidator;
using FluentValidation.Results;

namespace RBlazeLabs.Common.Validators
{

    /// <summary>
    /// Brasilian document cpf/cnpj validator
    /// </summary>
    public class BrCpfCnpjValidator : AbstractValidator<string>
    {

        /// <summary>
        /// Create a new instance of object
        /// </summary>
        /// <param name="fieldName">Field name</param>
        /// <param name="isRequired">Indicates whether the CPF/CNPJ is mandatory</param>
        public BrCpfCnpjValidator(string fieldName, bool isRequired)
        {

            RuleFor(doc => doc)
                .NotEmpty()
                    .When(doc => isRequired)
                    .WithMessage(this.GetMessage<DV>("DOCUMENT_REQUIRED", MD.DOCUMENT_REQUIRED, fieldName))
                .Custom((doc, ctx) =>
                {
                    if (!Doc.CheckDocument(doc))
                        ctx.AddFailure(new ValidationFailure(
                            nameof(fieldName), 
                            this.GetMessage<DV>("DOCUMENT_INVALID", MD.DOCUMENT_INVALID, fieldName)
                        ));
                });
            ;

        }

    }
}
