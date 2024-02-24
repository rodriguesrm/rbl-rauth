using Microsoft.EntityFrameworkCore;
using RBlazeLabs.Architecture.Domain.Entities;
using RBlazeLabs.Architecture.Exceptions;

namespace RBlazeLabs.Architecture.Infra.Data
{

    /// <summary>
    /// Abstract repository base
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    public abstract class RepositoryProviderBase<TEntity> : IRepositoryBase<TEntity>
        where TEntity : EntityBase<TEntity>
    {

        #region Local objects/variables

        /// <summary>
        /// Database context object
        /// </summary>
        protected readonly DbContext _ctx;

        /// <summary>
        /// Dbset object
        /// </summary>
        protected readonly DbSet<TEntity> _dbSet;

        #endregion

        #region Constructors

        /// <summary>
        /// Create a new repository instance
        /// </summary>
        /// <param name="ctx">Data base context object</param>
        public RepositoryProviderBase(DbContext ctx)
        {
            _ctx = ctx;
            _dbSet = _ctx.Set<TEntity>();
        }

        #endregion

        #region Abstract methods

        /// <summary>
        /// Checks if the entity has the same key that was informed (double check)
        /// </summary>
        /// <param name="key">Verification key value</param>
        /// <param name="entity">Instance of key</param>
        protected abstract bool IsKeyOk(int key, TEntity entity);

        #endregion

        #region Public Methods

        ///<inheritdoc/>
        public virtual async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (!entity.IsValid)
                throw new InvalidEntityException(nameof(entity));
            _ = await _dbSet.AddAsync(entity, cancellationToken).AsTask();
            return entity;
        }

        ///<inheritdoc/>
        public virtual TEntity Update(int key, TEntity entity)
        {
            if (!IsKeyOk(key, entity) || !entity.IsValid)
                throw new InvalidEntityException(nameof(entity));
            return _dbSet.Update(entity).Entity;
        }

        ///<inheritdoc/>
        public virtual async Task<TEntity?> GetByKeyAsync(int key, CancellationToken cancellationToken = default)
        {

            cancellationToken.ThrowIfCancellationRequested();
            TEntity? entity = await Task.Run(() => _dbSet.Find(key));
            cancellationToken.ThrowIfCancellationRequested();

            return entity;

        }

        ///<inheritdoc/>
        public virtual async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
        {

            cancellationToken.ThrowIfCancellationRequested();
            IEnumerable<TEntity> entities = await _dbSet.ToListAsync(cancellationToken);
            cancellationToken.ThrowIfCancellationRequested();

            return entities;

        }

        ///<inheritdoc/>
        public virtual void Delete(int key)
        {
            TEntity? entity = _dbSet.Find(key);
            if (entity is not null)
            {
                if (entity is ISoftDeletion deletion)
                {
                    deletion.IsDeleted = true;
                }
                else
                {
                    _dbSet.Remove(entity);
                }
            }
        }

        #endregion

    }

}
