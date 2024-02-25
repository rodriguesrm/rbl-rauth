using RBlazeLabs.Common.Contracts.Entities;
using RBlazeLabs.Common.Resources;
using RBlazeLabs.Common.Validators;

namespace RBlazeLabs.Common.ValueObjects
{

    /// <summary>
    /// Audit value object model
    /// </summary>
    public class Audit : BaseVO, IAuditAuthor
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
            if (CreatedAuthor is null)
                AddNotification(
                    nameof(CreatedAuthor),
                    this.GetMessage<SharedLanguageResource>
                    (
                        "CREATED_AUTHOR_REQUIRED", 
                        "{0} is required", 
                        nameof(CreatedAuthor)
                    )
                ); ;
            if (CreatedAuthor is not null) AddNotifications(CreatedAuthor.Notifications);
            if (ChangedAuthor is not null) AddNotifications(ChangedAuthor.Notifications);
        }

        #endregion

    }
}