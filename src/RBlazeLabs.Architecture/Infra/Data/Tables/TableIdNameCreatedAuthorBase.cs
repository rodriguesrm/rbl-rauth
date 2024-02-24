namespace RBlazeLabs.Architecture.Infra.Data.Tables
{

    /// <summary>
    /// Abstract table entity class with id and description column
    /// </summary>
    /// <typeparam name="TTable">Table entity type</typeparam>
    public abstract class TableIdNameCreatedAuthorBase<TTable> : TableIdCreatedAuthorBase<TTable>
        where TTable : TableIdCreatedAuthorBase<TTable>
    {

        #region Constructors

        /// <summary>
        /// Create a new table entity instance
        /// </summary>
        public TableIdNameCreatedAuthorBase() : base() { }

        /// <summary>
        /// Create a new table entity instance
        /// </summary>
        /// <param name="id">Id value</param>
        /// <param name="name">Name value</param>
        public TableIdNameCreatedAuthorBase(int id, string name) : base(id)
        {
            Name = name;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Table name value
        /// </summary>
        public string Name { get; set; } = string.Empty;

        #endregion

    }

}
