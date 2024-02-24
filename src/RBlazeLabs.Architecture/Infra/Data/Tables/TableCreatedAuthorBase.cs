namespace RBlazeLabs.Architecture.Infra.Data.Tables
{

    /// <summary>
    /// Table entity abstract class with id column
    /// </summary>
    /// <typeparam name="TTable">Table entity type</typeparam>
    public abstract class TableCreatedAuthorBase<TTable> : TableBase<TTable>, ITable, ICreatedAuthor
        where TTable : TableBase<TTable>
    {

        #region Constructors

        /// <summary>
        /// Create a new table instance
        /// </summary>
        public TableCreatedAuthorBase() : base() { }

        #endregion

        #region Properties

        /// <summary>
        /// Log creation date
        /// </summary>
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// Log creation user id
        /// </summary>
        int ICreatedAuthor.CreatedBy { get; set; }

        #endregion

    }

}
