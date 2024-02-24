namespace RBlazeLabs.Architecture.Infra.Data.Tables
{

    /// <summary>
    /// Abstract table entity class with id column
    /// </summary>
    /// <typeparam name="TTable">Table entity type</typeparam>
    public abstract class TableIdAuditBase<TTable> : TableIdBase<TTable>, IAudit
        where TTable : TableIdAuditBase<TTable>
    {

        #region Constructors

        /// <summary>
        /// Create a new table entity instance
        /// </summary>
        public TableIdAuditBase() : base() { }

        /// <summary>
        /// Create a new table entity instance
        /// </summary>
        /// <param name="id">Entity id</param>
        public TableIdAuditBase(int id) : base(id) { }

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
