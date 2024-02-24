namespace RBlazeLabs.Architecture.Infra.Data.Tables
{

    /// <summary>
    /// Abstract table entity class with id column
    /// </summary>
    public abstract class TableIdBase<TTable> : TableBase<TTable>
        where TTable : TableIdBase<TTable>
    {

        #region Constructors

        /// <summary>
        /// Create a new table entity instance
        /// </summary>
        public TableIdBase() { }


        /// <summary>
        /// Create a new table entity instance
        /// </summary>
        /// <param name="id">Table entity id value</param>
        public TableIdBase(int id)
        {
            Id = id;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Table entity id
        /// </summary>
        public int Id { get; protected set; }

        #endregion

        #region Public methods

        ///<inheritdoc/>
        public override string ToString()
            => $"{GetType().Name} - Id = {Id}";

        #endregion

    }

}