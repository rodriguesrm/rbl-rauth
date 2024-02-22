using RBlazeLabs.Common.Models;

namespace RBlazeLabs.Common.Dtos
{

    /// <summary>
    /// Abstract dto-audit-data model base object
    /// </summary>
    public abstract class AppDtoAuditBase : AppDtoBase
    {

        #region Properties

        /// <summary>
        /// Created author data
        /// </summary>
        public required AuditAuthor CreatedBy { get; set; }

        /// <summary>
        /// Changed author data
        /// </summary>
        public AuditAuthor? ChangedBy { get; set; }

        #endregion

    }

}