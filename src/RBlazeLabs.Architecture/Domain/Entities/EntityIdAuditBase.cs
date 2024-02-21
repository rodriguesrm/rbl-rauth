using RBlazeLabs.Common.Entities;
using RBlazeLabs.Common.ValueObjects;

namespace RBlazeLabs.Architecture.Domain.Entities
{

    /// <summary>
    /// Abstract entity class with id column
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    public abstract class EntityIdAuditBase<TEntity> : EntityIdBase<TEntity>, IAuditAuthor
        where TEntity : EntityIdAuditBase<TEntity>
    {

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        #region Constructors
        /// <summary>
        /// Create a new entity instance
        /// </summary>
        public EntityIdAuditBase() : base() { }

        /// <summary>
        /// Create a new entity instance
        /// </summary>
        /// <param name="id">Entity id</param>
        public EntityIdAuditBase(int id) : base(id) { }

        #endregion
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        #region Properties

        ///<inheritdoc/>
        public DateTime CreatedOn { get; set; }

        ///<inheritdoc/>
        public Author CreatedAuthor { get; set; }

        ///<inheritdoc/>
        public DateTime? ChangedOn { get; set; }

        ///<inheritdoc/>
        public AuthorNullable ChangedAuthor { get; set; }

        #endregion

    }

}