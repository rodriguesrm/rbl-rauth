namespace RBlazeLabs.Architecture.Infra.Data.Tables
{

    /// <summary>
    /// Table entity abstract class with id column
    /// </summary>
    /// <typeparam name="TTable">Table entity type</typeparam>
    /// <typeparam name="TKey">Table entity key</typeparam>
    public abstract class TableAuditBase<TTable> : TableBase<TTable>, ITable, IAudit
        where TTable : TableBase<TTable>
    {

        #region Constructors

        /// <summary>
        /// Create a new table instance
        /// </summary>
        public TableAuditBase() : base() { }

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

        /// <summary>
        /// Log change date
        /// </summary>
        public DateTime? ChangedOn { get; set; }

        /// <summary>
        /// Log change user id
        /// </summary>
        public int? ChangedBy { get; set; }

        #endregion

    }

}
