using RBlazeLabs.Common.Contracts.Entities;
using RBlazeLabs.Common.Validators;

namespace RBlazeLabs.Common.ValueObjects
{

    /// <summary>
    /// Name value-object model
    /// </summary>
    public class Name : BaseVO, IFullName
    {


        #region Constructors

        /// <summary>
        /// Create a new name-value-object instance
        /// </summary>
        /// <param name="firstName">First name</param>
        /// <param name="lastName">Last/Family name</param>
        public Name(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
            Validate();
        }

        #endregion

        #region Properties

        ///<inheritdoc/>
        public string FirstName { get; protected set; }

        ///<inheritdoc/>
        public string LastName { get; protected set; }

        ///<inheritdoc/>
        public string GetFullName()
            => $"{FirstName ?? string.Empty} {LastName ?? string.Empty}".Trim();

        #endregion

        #region Overrides

        ///<inheritdoc/>
        protected override void Validate()
            => this.ValidateModel<IFullName, FullNameValidator>(this);

        #endregion

    }

}
