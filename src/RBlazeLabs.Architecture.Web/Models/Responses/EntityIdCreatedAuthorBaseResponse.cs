using RBlazeLabs.Common.Contracts.Dtos;
using RBlazeLabs.Common.Models;

namespace RBlazeLabs.Architecture.Web.Models.Responses
{

    /// <summary>
    /// Abstract base model-reponse with audit-authors and id
    /// </summary>
    /// <param name="id">Key value</param>
    /// <param name="createdBy">Create author</param>
    public abstract class EntityIdCreatedAuthorBaseResponse(int id, AuditAuthor createdBy) 
        : EntityIdBaseResponse(id), ICreatedAuthorDto
    {

        #region Propriedades

        /// <summary>
        /// Created author data
        /// </summary>
        public AuditAuthor CreatedBy { get; set; } = createdBy;


        #endregion

    }

}
