using RBlazeLabs.Architecture.Domain.Entities;

namespace RBlazeLabs.Architecture
{

    /// <summary>
    /// Common methos and properties of domains and repositories
    /// </summary>
    public interface ICommonBase<TEntity>
        where TEntity : EntityBase<TEntity>
    {

        /// <summary>
        /// Add entity to context
        /// </summary>
        /// <param name="entity">Entity instance</param>
        /// <param name="cancellationToken">A System.Threading.CancellationToken to observe while waiting for the task to complete</param>
        Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// Update entity on context
        /// </summary>
        /// <param name="key">The values of the primary key for the entity to be found</param>
        /// <param name="entity">Entity instance</param>
        TEntity Update(int key, TEntity entity);

        /// <summary>
        /// Get row by key value
        /// </summary>
        /// <param name="key">The values of the primary key for the entity to be found</param>
        /// <param name="cancellationToken">A System.Threading.CancellationToken to observe while waiting for the task to complete</param>
        Task<TEntity?> GetByKeyAsync(int key, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get all entity rows
        /// </summary>
        /// <param name="cancellationToken">A System.Threading.CancellationToken to observe while waiting for the task to complete</param>
        Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Remove entity of context
        /// </summary>
        /// <param name="key">The values of the primary key for the entity to be found</param>
        void Delete(int key);

    }

}
