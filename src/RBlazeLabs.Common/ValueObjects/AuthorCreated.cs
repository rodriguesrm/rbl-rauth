using RBlazeLabs.Common.Abstractions;
using RBlazeLabs.Common.Contracts.Entities;
using RBlazeLabs.Common.Resources;

namespace RBlazeLabs.Common.ValueObjects
{

    /// <summary>
    /// Audit value object model
    /// </summary>
    public class AuthorCreated<TKey> : BaseVO, ICreatedAuthorData
    {


        #region Constructors

        /// <summary>
        /// Create a new Audit-Value-Object instance
        /// </summary>
        /// <param name="createdOn">Row created date</param>
        /// <param name="createdAuthor">Created author data</param>
        public AuthorCreated(DateTime createdOn, Author createdAuthor)
        {
            CreatedOn = createdOn;
            CreatedAuthor = createdAuthor;
            Validate();
        }

        #endregion

        #region Properties

        ///<inheritdoc/>
        public DateTime CreatedOn { get; set; }

        ///<inheritdoc/>
        public Author CreatedAuthor { get; set; }

        #endregion

        #region Overrides

        ///<inheritdoc/>
        protected override void Validate()
        {
            if (CreatedAuthor == null)
                AddNotification
                (
                    nameof(CreatedAuthor), 
                    ServiceActivator.GetStringInLocalizer<SharedLanguageResource>
                        ("CREATED_AUTHOR_REQUIRED", "{0} is required", 
                    nameof(CreatedAuthor))
                );
            if (CreatedAuthor != null) AddNotifications(CreatedAuthor.Notifications);
        }

        #endregion

    }
}