using FluentValidation;
using RBlazeLabs.Common.Contracts.Entities;
using MD = RBlazeLabs.Common.Validators.MessageValidationsDefault;
using AV = RBlazeLabs.Common.Validators.AddressValidator;

namespace RBlazeLabs.Common.Validators
{

    /// <summary>
    /// Address validator
    /// </summary>
    /// <remarks>
    /// Street name is required and has 2 and 80 characters.<br/>
    /// Address number is required and has 20 maximum characters.<br/>
    /// Secondary address is optional, but has 40 maxium characters.<br/>
    /// District/Neighborhood is required and 2 and 50 characters.<br/>
    /// City is required and 2 and 80 characters.<br/>
    /// State is required and has 2 characters.<br/>
    /// Zip code is required and has 8 digits (only numbers)
    /// </remarks>
    public class AddressValidator : AbstractValidator<IAddress>
    {

        /// <summary>
        /// Create a new instance of object
        /// </summary>
        /// <param name="address">Address object instance</param>
        /// <param name="isRequired">Indicate if address is required (default=true)</param>
        public AddressValidator(bool isRequired = true)
        {

            RuleFor(r => r.StreetName)
                .NotEmpty().When(a => isRequired)
                    .WithMessage(this.GetMessage<AV>("STREET_NAME_REQUIRED", MD.STREET_NAME_REQUIRED))
                .MinimumLength(2).When(a => isRequired || a.StreetName?.Length > 0)
                    .WithMessage(this.GetMessage<AV>("STREET_NAME_MIN_SIZE", MD.STREET_NAME_MIN_SIZE))
                .MaximumLength(80)
                    .WithMessage(this.GetMessage<AV>("STREET_NAME_MAX_SIZE", MD.STREET_NAME_MAX_SIZE))
            ;

            RuleFor(r => r.AddressNumber)
                .NotEmpty().When(a => isRequired)
                    .WithMessage(this.GetMessage<AV>("ADDRESS_NUMBER_REQUIRED", MD.ADDRESS_NUMBER_REQUIRED))
                .MaximumLength(20)
                    .WithMessage(this.GetMessage<AV>("ADDRESS_NUMBER_MAX_SIZE", MD.ADDRESS_NUMBER_MAX_SIZE))
            ;

            RuleFor(r => r.SecondaryAddress)
                .MaximumLength(40)
                    .WithMessage(this.GetMessage<AV>("SECONDARY_ADDRESS_MAX_SIZE", MD.SECONDARY_ADDRESS_MAX_SIZE))
            ;

            RuleFor(r => r.District)
                .NotEmpty().When(a => isRequired)
                    .WithMessage(this.GetMessage<AV>("DISTRICT_REQUIRED", MD.DISTRICT_REQUIRED))
                .MinimumLength(2).When(a => isRequired || a.District?.Length > 0)
                    .WithMessage(this.GetMessage<AV>("DISTRICT_MIN_SIZE", MD.DISTRICT_MIN_SIZE))
                .MaximumLength(50)
                    .WithMessage(this.GetMessage<AV>("DISTRICT_MAX_SIZE", MD.DISTRICT_MAX_SIZE))
            ;

            RuleFor(r => r.City)
                .NotEmpty().When(a => isRequired)
                    .WithMessage(this.GetMessage<AV>("CITY_REQUIRED", MD.CITY_REQUIRED))
                .MinimumLength(2).When(a => isRequired || a.City?.Length > 0)
                    .WithMessage(this.GetMessage<AV>("CITY_MIN_SIZE", MD.CITY_MIN_SIZE))
                .MaximumLength(80)
                    .WithMessage(this.GetMessage<AV>("CITY_MAX_SIZE", MD.CITY_MAX_SIZE))
            ;

            RuleFor(r => r.State)
                .NotEmpty().When(a => isRequired)
                    .WithMessage(this.GetMessage<AV>("STATE_REQUIRED", MD.STATE_REQUIRED))
                .Length(2).When(a => isRequired || a.State?.Length > 0)
                    .WithMessage(this.GetMessage<AV>("STATE_SIZE", MD.STATE_SIZE))
            ;

            RuleFor(r => r.ZipCode)
                .NotNull().When(a => isRequired)
                    .WithMessage(this.GetMessage<AV>("ZIP_CODE_REQUIRED", MD.ZIP_CODE_REQUIRED))
                .Length(8).When(a => isRequired || a.ZipCode?.Length > 0)
                    .WithMessage(this.GetMessage<AV>("ZIP_COD_SIZE", MD.ZIP_COD_SIZE))
                .Matches("[0-9]{8}").When(a => isRequired || a.ZipCode?.Length > 0)
                    .WithMessage(this.GetMessage<AV>("ZIP_CODE_ONLY_NUMBERS", MD.ZIP_CODE_ONLY_NUMBERS))
            ;

        }

    }
}
