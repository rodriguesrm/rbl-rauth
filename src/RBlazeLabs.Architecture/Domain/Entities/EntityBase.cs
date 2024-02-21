using FluentValidator;
using RBlazeLabs.Common.Entities;

namespace RBlazeLabs.Architecture.Domain.Entities
{

    /// <summary>
    /// Entity abstract class
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    public abstract class EntityBase<TEntity> : Notifiable, IEntity
        where TEntity : EntityBase<TEntity>
    {

        #region Constructors

        /// <summary>
        /// Create a new entity instance
        /// </summary>
        protected EntityBase() : base() { }

        #endregion

        #region Public methods

        /// <summary>
        /// Execute validate
        /// </summary>
        public abstract void Validate();

        /// <summary>
        /// Compare two objects
        /// </summary>
        /// <param name="obj">Object to compare</param>
        public override bool Equals(object? obj)
        {
            var compareTo = obj as EntityBase<TEntity>;

            if (ReferenceEquals(this, compareTo)) return true;
            if (compareTo is null) return false;
            return false;
        }

        /// <summary>
        /// Compare two objects
        /// </summary>
        /// <param name="a">First object to compare</param>
        /// <param name="b">Second object to compare</param>
        public static bool operator ==(EntityBase<TEntity> a, EntityBase<TEntity> b)
        {
            if (a is null && b is null) return true;
            if (a is null || b is null) return false;
            return a.Equals(b);
        }

        /// <summary>
        /// Compare two objects
        /// </summary>
        /// <param name="a">First object to compare</param>
        /// <param name="b">Second object to compare</param>
        public static bool operator !=(EntityBase<TEntity> a, EntityBase<TEntity> b)
            => !(a == b);

        /// <summary>
        /// Get HashCode 
        /// </summary>
        public override int GetHashCode()
            => GetType().GetHashCode() * 976;

        /// <summary>
        /// Get object string
        /// </summary>
        public override string ToString()
            => $"{GetType().Name}";

        /// <summary>
        /// Get object entity name
        /// </summary>
        public virtual string GetName()
            => GetType().Name
                .Split(".")
                .ToList()
                .Last();

        #endregion

    }

}
