using RBlazeLabs.Common.Abstractions;
using RBlazeLabs.Common.Entities;
using RBlazeLabs.Common.Resources;
using RBlazeLabs.Common.ValueObjects;

namespace RSoft.Lib.Common.ValueObjects
{

    /// <summary>
    /// Audit value object model
    /// </summary>
    public class Audit<TKey> : BaseVO, IAuditAuthor
    {


        #region Constructors

        /// <summary>
        /// Create a new Audit-Value-Object instance
        /// </summary>
        /// <param name="createdOn">Row created date</param>
        /// <param name="createdAuthor">Created author data</param>
        /// <param name="changedOn">Row changed date</param>
        /// <param name="changeAuthor">Last change author data</param>
        public Audit(DateTime createdOn, Author createdAuthor, DateTime? changedOn, AuthorNullable changeAuthor)
        {
            CreatedOn = createdOn;
            CreatedAuthor = createdAuthor;
            ChangedOn = changedOn;
            ChangedAuthor = changeAuthor;
            Validate();
        }

        #endregion

        #region Properties

        ///<inheritdoc/>
        public DateTime CreatedOn { get; set; }

        ///<inheritdoc/>
        public Author CreatedAuthor { get; set; }

        ///<inheritdoc/>
        public DateTime? ChangedOn { get; set; }

        ///<inheritdoc/>
        public AuthorNullable ChangedAuthor { get; set; }

        #endregion

        #region Overrides

        ///<inheritdoc/>
        protected override void Validate()
        {
            if (CreatedAuthor == null)
                AddNotification(
                    nameof(CreatedAuthor), 
                    ServiceActivator.GetStringInLocalizer<SharedLanguageResource>(
                        "CREATED_AUTHOR_REQUIRED", "{0} is required", 
                        nameof(CreatedAuthor)
                    )
                );
            if (CreatedAuthor != null) AddNotifications(CreatedAuthor.Notifications);
            if (ChangedAuthor != null) AddNotifications(ChangedAuthor.Notifications);
        }

        #endregion

    }
}