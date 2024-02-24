namespace RBlazeLabs.Architecture.Infra.Data.Tables
{

    /// <summary>
    /// Abstract table entity class with id column
    /// </summary>
    /// <typeparam name="TTable">Table entity type</typeparam>
    public abstract class TableIdCreatedAuthorBase<TTable> : TableIdBase<TTable>, ICreatedAuthor
        where TTable : TableIdCreatedAuthorBase<TTable>
    {

        #region Constructors

        /// <summary>
        /// Create a new table entity instance
        /// </summary>
        public TableIdCreatedAuthorBase() : base() { }

        /// <summary>
        /// Create a new table entity instance
        /// </summary>
        /// <param name="id">Entity id</param>
        public TableIdCreatedAuthorBase(int id) : base(id) { }

        #endregion

        #region Properties

        /// <summary>
        /// Log creation date
        /// </summary>
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// Log creation user id
        /// </summary>
        public int CreatedBy { get; set; }

        #endregion

    }

}
