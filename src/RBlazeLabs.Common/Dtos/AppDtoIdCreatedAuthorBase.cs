using RBlazeLabs.Common.Models;

namespace RBlazeLabs.Common.Dtos
{

    /// <summary>
    /// Abstract dto-id-audit model base object
    /// </summary>
    public abstract class AppDtoIdCreatedAuthorBase : AppDtoIdBase
    {

        #region Properties

        /// <summary>
        /// Created author data
        /// </summary>
        public required AuditAuthor CreatedBy { get; set; }

        #endregion

    }

}
