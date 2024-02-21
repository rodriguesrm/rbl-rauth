using RBlazeLabs.Common.Entities;
using RBlazeLabs.Common.ValueObjects;

namespace RBlazeLabs.Architecture.Domain.Entities
{

    /// <summary>
    /// Entity abstract class with id column
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    public abstract class EntityAuditBase<TEntity> : EntityBase<TEntity>, IEntity, IAuditAuthor
        where TEntity : EntityBase<TEntity>
    {

        #region Constructors

        /// <summary>
        /// Create a new entity instance
        /// </summary>
        public EntityAuditBase() : base() { }

        #endregion

        #region Properties

        ///<inheritdoc/>
        public DateTime CreatedOn { get; set; }

        ///<inheritdoc/>
        public Author CreatedAuthor { get; set; } = new(0, string.Empty);

        ///<inheritdoc/>
        public DateTime? ChangedOn { get; set; }

        ///<inheritdoc/>
        public AuthorNullable ChangedAuthor { get; set; } = new(0, string.Empty);

        #endregion

    }

}