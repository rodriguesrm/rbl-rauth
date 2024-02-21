using RBlazeLabs.Architecture.Domain.Entities;
using RBlazeLabs.Common.Entities;
using RBlazeLabs.Common.ValueObjects;

namespace RSoft.Lib.Design.Domain.Entities
{

    /// <summary>
    /// Entity abstract class
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    public abstract class EntityCreatedAuthorBase<TEntity> : EntityBase<TEntity>, IEntity, ICreatedAuthor
        where TEntity : EntityBase<TEntity>
    {

#pragma warning disable CS8618
        #region Constructors

        /// <summary>
        /// Create a new entity instance
        /// </summary>
        public EntityCreatedAuthorBase() : base() { }

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