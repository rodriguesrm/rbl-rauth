using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace RBlazeLabs.Architecture.Infra.Data
{

    /// <summary>
    /// Unit of work object to maintain the integrity of transactional operations
    /// </summary>
    /// <remarks>
    /// Create a new UnitOfWork instance
    /// </remarks>
    /// <param name="ctx">Database context object</param>
    public abstract class UnitOfWorkBase(DbContext ctx) : IUnitOfWork
    {

        #region Local objects/variables

        private readonly DbContext _ctx = ctx;
        private IDbContextTransaction? _transaction;

        #endregion

        #region Properties

        /// <summary>
        /// Flag to indicate whether the transaction was started
        /// </summary>
        public bool TransactionStarted => _transaction is null;

        #endregion

        #region Public methods

        ///<inheritdoc/>
        public int SaveChanges()
            => _ctx.SaveChanges();

        ///<inheritdoc/>
        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return await _ctx.SaveChangesAsync(cancellationToken);
        }

        ///<inheritdoc/>
        public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            if (_transaction is not null) throw new InvalidOperationException("The transaction has already started");
            cancellationToken.ThrowIfCancellationRequested();
            await _ctx.Database.BeginTransactionAsync(cancellationToken)
                .ContinueWith((tsk) =>
                {
                    _transaction = tsk.Result;
                }, cancellationToken);
        }

        ///<inheritdoc/>
        public async Task CommitAsync(CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (_transaction is null) throw new InvalidOperationException("The transaction has not started");
            await _transaction.CommitAsync(cancellationToken);

        }

        ///<inheritdoc/>
        public async Task RollBackAsync(CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (_transaction is null) throw new InvalidOperationException("The transaction has not started");
            await _transaction.RollbackAsync(cancellationToken);
        }

        #endregion

    }

}
