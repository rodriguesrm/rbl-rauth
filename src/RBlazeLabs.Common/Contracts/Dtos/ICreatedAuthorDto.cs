using RBlazeLabs.Common.Models;

namespace RBlazeLabs.Common.Contracts.Dtos
{

    /// <summary>
    /// Created author Dto interface
    /// </summary>
    public interface ICreatedAuthorDto
    {

        /// <summary>
        /// Created author data
        /// </summary>
        AuditAuthor CreatedBy { get; set; }

    }

}
