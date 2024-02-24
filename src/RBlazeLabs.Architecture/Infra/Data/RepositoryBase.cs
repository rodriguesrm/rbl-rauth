using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using RBlazeLabs.Architecture.Domain.Entities;
using RBlazeLabs.Architecture.Exceptions;
using RBlazeLabs.Architecture.Infra.Data.Tables;
using System.Collections;

namespace RBlazeLabs.Architecture.Infra.Data
{

    /// <summary>
    /// Abstract repository base
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    /// <typeparam name="TTable">Table type</typeparam>
    public abstract class RepositoryBase<TEntity, TTable> : IRepositoryBase<TEntity>
        where TEntity : EntityBase<TEntity>
        where TTable : TableBase<TTable>
    {

        #region Local objects/variables

        /// <summary>
        /// Database context object
        /// </summary>
        protected readonly DbContext _ctx;

        /// <summary>
        /// Dbset object
        /// </summary>
        protected readonly DbSet<TTable> _dbSet;

        #endregion

        #region Constructors

        /// <summary>
        /// Create a new repository instance
        /// </summary>
        /// <param name="ctx">Data base context object</param>
        public RepositoryBase(DbContext ctx)
        {
            _ctx = ctx;
            _dbSet = _ctx.Set<TTable>();
        }

        #endregion

        #region Abstract methods

        /// <summary>
        /// Map table to entity
        /// </summary>
        /// <param name="table">Table object</param>
        protected abstract TEntity Map(TTable table);

        /// <summary>
        /// Map entity to table for add action
        /// </summary>
        /// <param name="entity">Domain Entity object</param>
        protected abstract TTable MapForAdd(TEntity entity);

        /// <summary>
        /// Map entity to table for update action
        /// </summary>
        /// <param name="entity">Domain entity object</param>
        /// <param name="table">Table entity object</param>
        protected abstract TTable MapForUpdate(TEntity entity, TTable table);

        #endregion

        #region Public Methods

        ///<inheritdoc/>
        public virtual async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (!entity.IsValid)
                throw new InvalidEntityException(nameof(entity));
            TTable table = MapForAdd(entity);
            EntityEntry<TTable> tsk = await _dbSet.AddAsync(table, cancellationToken).AsTask();
            entity = Map(tsk.Entity);
            return entity;
        }

        ///<inheritdoc/>
        public virtual TEntity Update(int key, TEntity entity)
        {

            if (!entity.IsValid)
                throw new InvalidEntityException(nameof(entity));

            TTable? table = _dbSet.Find(key) ?? throw new InvalidOperationException(
                    $"[{key}] The data update operation cannot be completed because the " +
                    $"entity does not exist in the database. The same may have been deleted."
                );
            table = MapForUpdate(entity, table);
            table = _dbSet.Update(table).Entity;

            entity = Map(table);
            return entity;
        }

        ///<inheritdoc/>
        public virtual async Task<TEntity?> GetByKeyAsync(int key, CancellationToken cancellationToken = default)
        {

            cancellationToken.ThrowIfCancellationRequested();

            TTable? table = await Task.Run(() => _dbSet.Find(key));
            TEntity? entity = table is null ? null : Map(table);

            cancellationToken.ThrowIfCancellationRequested();

            return entity;

        }

        ///<inheritdoc/>
        public virtual async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
        {

            cancellationToken.ThrowIfCancellationRequested();

            IEnumerable<TTable> rows = await _dbSet.ToListAsync(cancellationToken);
            IEnumerable<TEntity> entities = rows.Select(r => Map(r)).ToList();

            cancellationToken.ThrowIfCancellationRequested();

            return entities;

        }

        ///<inheritdoc/>
        public virtual void Delete(int key)
        {
            TTable? table = _dbSet.Find(key);
            if (table is not null)
            {
                if (table is ISoftDeletion deletion)
                {
                    //TODO: Verify soft deletion mechanism e improve
                    deletion.IsDeleted = true;
                }
                else
                {
                    _dbSet.Remove(table);
                }
            }
        }

        #endregion

    }

}
