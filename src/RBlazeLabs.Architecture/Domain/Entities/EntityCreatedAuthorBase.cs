using RBlazeLabs.Architecture.Domain.Entities;
using RSoft.Lib.Common.Contracts.Entities;
using RSoft.Lib.Common.ValueObjects;
using System;

namespace RSoft.Lib.Design.Domain.Entities
{

    /// <summary>
    /// Entity abstract class
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    public abstract class EntityCreatedAuthorBase<TEntity> : EntityBase<TEntity>, IEntity, ICreatedAuthor
        where TEntity : EntityBase<TEntity>
    {

        #region Constructors

        /// <summary>
        /// Create a new entity instance
        /// </summary>
        public EntityCreatedAuthorBase() : base() { }

        #endregion

        #region Properties

        ///<inheritdoc/>
        public DateTime CreatedOn { get; set; }

        ///<inheritdoc/>
        public Author CreatedAuthor { get; set; }

        #endregion

    }

}