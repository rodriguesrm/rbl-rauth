namespace RSoft.Lib.Design.Domain.Entities
{

    /// <summary>
    /// Abstract entity class with id and description column
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    public abstract class EntityIdNameAuditBase<TEntity> : EntityIdAuditBase<TEntity>
        where TEntity : EntityIdAuditBase<TEntity>
    {

        #region Constructors

        /// <summary>
        /// Create a new entity instance
        /// </summary>
        public EntityIdNameAuditBase() : base() { }

        /// <summary>
        /// Create a new entity instance
        /// </summary>
        /// <param name="id">Id value</param>
        /// <param name="name">Name value</param>
        public EntityIdNameAuditBase(int id, string name) : base(id)
        {
            Name = name;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Entity name value
        /// </summary>
        public string Name { get; set; }

        #endregion

    }

}
