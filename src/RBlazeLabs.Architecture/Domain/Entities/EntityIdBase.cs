namespace RBlazeLabs.Architecture.Domain.Entities
{

    /// <summary>
    /// Abstract entity class with id column
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    public abstract class EntityIdBase<TEntity> : EntityBase<TEntity>
        where TEntity : EntityIdBase<TEntity>
    {

        #region Constructors

        /// <summary>
        /// Create a new entity instance
        /// </summary>
        public EntityIdBase() : base() { }

        /// <summary>
        /// Create a new entity instance
        /// </summary>
        /// <param name="id">Entity id</param>
        public EntityIdBase(int id) : base()
        {
            Id = id;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Entity id
        /// </summary>
        public int? Id { get; protected set; }

        #endregion

        #region Overrides

        /// <summary>
        /// Compare two objects
        /// </summary>
        /// <param name="obj">Object to compare</param>
        public override bool Equals(object? obj)
        {
            var compareTo = obj as EntityIdBase<TEntity>;

            if (ReferenceEquals(this, compareTo)) return true;
            if (compareTo is null) return false;
            if (Id is null) return false;

            return Id.Equals(compareTo.Id);
        }

        ///<inheritdoc/>
        public override int GetHashCode()
            => GetType().GetHashCode() * 976;

        ///<inheritdoc/>
        public override string ToString()
            => $"{GetType().Name} - Id = {Id}";

        #endregion

    }

}
