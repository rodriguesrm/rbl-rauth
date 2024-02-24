using RBlazeLabs.Architecture.Domain.Entities;
using RBlazeLabs.Architecture.Infra.Data;
using RBlazeLabs.Common.Contracts.Web;

namespace RBlazeLabs.Architecture.Domain.Services
{

    /// <summary>
    /// Abstract class domain service
    /// </summary>
    /// <typeparam name="TEntity">Entity tipe</typeparam>
    /// <typeparam name="TRepository">Repository type</typeparam>
    /// <remarks>
    /// Create a new domain service instance
    /// </remarks>
    /// <param name="repository">Principal repository</param>
    /// <param name="authenticatedUser">Authenticated user</param>
    public abstract class DomainServiceBase<TEntity, TRepository>
    (
        TRepository repository, 
        IAuthenticatedUser authenticatedUser
    ) : IDomainServiceBase<TEntity>
        where TEntity : EntityBase<TEntity>
        where TRepository : class, IRepositoryBase<TEntity>
    {

        #region Local objects/variables

        /// <summary>
        /// Repository object
        /// </summary>
        protected TRepository _repository = repository;

        /// <summary>
        /// Authenticated user object
        /// </summary>
        protected IAuthenticatedUser _authenticatedUser = authenticatedUser;

        #endregion
        #region Constructors

        #endregion

        #region Abstract methods

        /// <summary>
        /// Prepare save
        /// </summary>
        /// <param name="entity">Entity to save</param>
        /// <param name="isUpdate">Flag to indicate update action</param>
        public abstract void PrepareSave(TEntity entity, bool isUpdate);

        #endregion

        #region Public methods

        ///<inheritdoc/>
        public virtual async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            if (!entity.IsValid) return entity;
            if (cancellationToken.IsCancellationRequested)
            {
                entity.AddNotification(entity.GetName(), "Operation was cancelled");
                return entity;
            }
            PrepareSave(entity, false);
            TEntity savedEntity = await _repository.AddAsync(entity, cancellationToken);
            return savedEntity;
        }


        ///<inheritdoc/>
        public virtual TEntity Update(int key, TEntity entity)
        {
            if (!entity.IsValid) return entity;
            PrepareSave(entity, true);
            TEntity savedEntity = _repository.Update(key, entity);
            return savedEntity;
        }

        ///<inheritdoc/>
        public virtual async Task<TEntity?> GetByKeyAsync(int key, CancellationToken cancellationToken = default)
            => await _repository.GetByKeyAsync(key, cancellationToken);

        ///<inheritdoc/>
        public virtual async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken)
            => await _repository.GetAllAsync(cancellationToken);

        ///<inheritdoc/>
        public virtual void Delete(int key)
            => _repository.Delete(key);

        #endregion

    }

}