using RBlazeLabs.Common.Entities;
using RBlazeLabs.Common.ValueObjects;

namespace RBlazeLabs.Architecture.Domain.Entities
{

    /// <summary>
    /// Abstract entity class with id column
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    public abstract class EntityIdCreatedAuthorBase<TEntity> : EntityIdBase<TEntity>, ICreatedAuthor
        where TEntity : EntityIdCreatedAuthorBase<TEntity>
    {

#pragma warning disable CS8618
        #region Constructors

        /// <summary>
        /// Create a new entity instance
        /// </summary>
        public EntityIdCreatedAuthorBase() : base() { }

        /// <summary>
        /// Create a new entity instance
        /// </summary>
        /// <param name="id">Entity id</param>
        public EntityIdCreatedAuthorBase(int id) : base(id) { }

        #endregion
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        #region Properties

        ///<inheritdoc/>
        public DateTime CreatedOn { get; set; }

        ///<inheritdoc/>
        public Author CreatedAuthor { get; set; }

        #endregion

    }

}