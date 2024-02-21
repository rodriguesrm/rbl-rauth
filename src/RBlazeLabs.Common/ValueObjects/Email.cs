﻿using RBlazeLabs.Common.Validations.Constracts;

namespace RBlazeLabs.Common.ValueObjects
{

    /// <summary>
    /// E-email value-object model
    /// </summary>
    public class Email : BaseVO
    {

        #region Local objects/variables

        private readonly bool _required;

        #endregion

        #region Constructors

        /// <summary>
        /// Creata a new instance of email-object-value instance
        /// </summary>
        /// <param name="address">E-mail address</param>
        public Email(string address) : this(address, true) { }

        /// <summary>
        /// Creata a new instance of email-object-value instance
        /// </summary>
        /// <param name="address">E-mail address</param>
        /// <param name="required">Indicates whether email is required</param>
        public Email(string address, bool required)
        {
            Address = address;
            _required = required;
            Validate();
        }

        #endregion

        #region Properties

        /// <summary>
        /// E-mail address
        /// </summary>
        public string? Address { get; protected set; }

        /// <summary>
        /// Get login from email (before @)
        /// </summary>
        public string? Login => Address?.Split('@').ToList().FirstOrDefault();

        /// <summary>
        /// Get domain from email (after @)
        /// </summary>
        public string? Domain => Address?.Split('@').ToList().LastOrDefault();

        #endregion

        #region Overrides

        ///<inheritdoc/>
        protected override void Validate()
        {
            AddNotifications(new EmailValidationContract(Address, _required).Contract.Notifications);
        }

        #endregion

    }
}
