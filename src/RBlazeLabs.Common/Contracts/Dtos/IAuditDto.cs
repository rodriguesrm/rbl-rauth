using RBlazeLabs.Common.Models;

namespace RBlazeLabs.Common.Contracts.Dtos
{

    /// <summary>
    /// Audit Dto interface
    /// </summary>
    public interface IAuditDto
    {

        /// <summary>
        /// Created author data
        /// </summary>
        AuditAuthor CreatedBy { get; set; }

        /// <summary>
        /// Changed author data
        /// </summary>
        AuditAuthor? ChangedBy { get; set; }

    }

}
