namespace RBlazeLabs.Architecture.Infra.Data
{

    /// <summary>
    /// Unit of work interface (transaction)
    /// </summary>
    public interface IUnitOfWork
    {

        #region Properties

        /// <summary>
        /// Indicates whether the transaction was initiated
        /// </summary>
        public bool TransactionStarted { get; }

        #endregion

        #region Methods

        /// <summary>
        /// Saves all changes made in this context to the database
        /// </summary>
        int SaveChanges();

        /// <summary>
        /// Saves all changes made in this context to the database
        /// </summary>
        /// <param name="cancellationToken">A System.Threading.CancellationToken to observe while waiting for the task to complete</param>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Start a transaction
        /// </summary>
        /// <param name="cancellationToken">A System.Threading.CancellationToken to observe while waiting for the task to complete</param>
        /// <exception cref="InvalidOperationException">Occurs when the transaction has already started</exception>"
        Task BeginTransactionAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Commit transaction
        /// </summary>
        /// <param name="cancellationToken">A System.Threading.CancellationToken to observe while waiting for the task to complete</param>
        /// <exception cref="InvalidOperationException">Occurs when the transaction has not started</exception>"
        Task CommitAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Commit transaction
        /// </summary>
        /// <param name="cancellationToken">A System.Threading.CancellationToken to observe while waiting for the task to complete</param>
        /// <exception cref="InvalidOperationException">Occurs when the transaction has not started</exception>"
        Task RollBackAsync(CancellationToken cancellationToken = default);

        #endregion


    }

}