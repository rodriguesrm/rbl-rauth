using RBlazeLabs.Common.Abstractions;

namespace RBlazeLabs.Common.Validations.Constracts
{

    /// <summary>
    /// Period date validation contract. Validate if date is between in period.
    /// </summary>
    public class PeriodDateValidationContract : BaseValidationContract
    {

        #region Constructors

        /// <summary>
        /// Create a new instance of contract
        /// </summary>
        /// <param name="startDate">Start date to validate</param>
        /// <param name="endDate">End date to validate</param>
        /// <param name="field">Field name</param>
        public PeriodDateValidationContract(DateTime? startDate, DateTime? endDate, string field) : base()
        {

            Contract
                .IsNotNull(startDate, field, 
                    ServiceActivator.GetStringInLocalizer<PeriodDateValidationContract>(
                        "START_DATE_INVALID", "[{0}] Invalid start date", field))
                .IsNotNull(endDate, field, 
                    ServiceActivator.GetStringInLocalizer<PeriodDateValidationContract>(
                        "END_DATE_INVALID", "[{0}] Invalid end date", field));

            if (startDate.HasValue && endDate.HasValue)
                Contract
                    .IsGreaterOrEqualsThan(endDate.Value, startDate.Value, field, 
                        ServiceActivator.GetStringInLocalizer<PeriodDateValidationContract>(
                            "DATES_INVALID", "[{0}] The end date must be greater than the start date", field))
            ;

        }

        #endregion

    }
}
