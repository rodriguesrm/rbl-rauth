using RBlazeLabs.Common.Contracts.Dtos;
using RBlazeLabs.Common.Models;

namespace RBlazeLabs.Architecture.Web.Models.Responses
{

    /// <summary>
    /// Abstract base model-reponse with audit-authors and id
    /// </summary>
    /// <param name="id">Key value</param>
    /// <param name="createdBy">Create author</param>
    /// <param name="changedBy">Change author</param>
    public abstract class EntityIdAuditBaseResponse(int id, AuditAuthor createdBy, AuditAuthor changedBy) 
        : EntityIdBaseResponse(id), IAuditDto
    {

        #region Properties

        /// <summary>
        /// Created author data
        /// </summary>
        public AuditAuthor CreatedBy { get; set; } = createdBy;

        /// <summary>
        /// Changed author data
        /// </summary>
        public AuditAuthor? ChangedBy { get; set; } = changedBy;

        #endregion

    }

}
