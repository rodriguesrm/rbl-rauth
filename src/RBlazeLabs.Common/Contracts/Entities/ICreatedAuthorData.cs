using RBlazeLabs.Common.ValueObjects;

namespace RBlazeLabs.Common.Contracts.Entities
{

    /// <summary>
    /// Created author interface
    /// </summary>
    public interface ICreatedAuthorData
    {

        /// <summary>
        /// Row create date
        /// </summary>
        DateTime CreatedOn { get; set; }

        /// <summary>
        /// Created author data
        /// </summary>
        Author CreatedAuthor { get; set; }

    }
}