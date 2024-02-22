using RBlazeLabs.Common.ValueObjects;

namespace RBlazeLabs.Common.Contracts.Entities
{

    /// <summary>
    /// Audit author interface
    /// </summary>
    public interface IAuditAuthor
    {

        /// <summary>
        /// Row create date
        /// </summary>
        DateTime CreatedOn { get; set; }

        /// <summary>
        /// Created author data
        /// </summary>
        Author CreatedAuthor { get; set; }

        /// <summary>
        /// Row changed date
        /// </summary>
        DateTime? ChangedOn { get; set; }

        /// <summary>
        /// Last change author data
        /// </summary>
        AuthorNullable ChangedAuthor { get; set; }

    }
}