using RBlazeLabs.Common.ValueObjects;

namespace RBlazeLabs.Common.Entities
{

    /// <summary>
    /// Created author interface
    /// </summary>
    public interface ICreatedAuthor
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