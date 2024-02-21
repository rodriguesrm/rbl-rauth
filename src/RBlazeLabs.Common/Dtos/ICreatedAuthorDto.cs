using RBlazeLabs.Common.Models;

namespace RSoft.Lib.Common.Contracts.Dtos
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
