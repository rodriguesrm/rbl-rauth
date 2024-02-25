using RBlazeLabs.Architecture.Domain.Entities;
using RBlazeLabs.Architecture.Domain.Services;
using RBlazeLabs.Architecture.Infra.Data;
using RBlazeLabs.Common.Contracts.Dtos;

namespace RBlazeLabs.Architecture.Application.Services
{

    /// <summary>
    /// Application service abstract class base
    /// </summary>
    /// <typeparam name="TDto">Dto type</typeparam>
    /// <typeparam name="TEntity">Entity type</typeparam>
    /// <param name="uow">Objeto de unidade de trabalho / controle de transações</param>
    /// <param name="dmn">Objeto de serviço de domínio</param>
    public abstract class AppServiceBase<TDto, TEntity>(IUnitOfWork uow, IDomainServiceBase<TEntity> dmn) : IAppServiceBase<TDto>
        where TDto : IAppDto
        where TEntity : EntityBase<TEntity>
    {

        #region Local objects/variables

        /// <summary>
        /// Unit of work object
        /// </summary>
        protected readonly IUnitOfWork _uow = uow;

        /// <summary>
        /// Domain service object
        /// </summary>
        protected readonly IDomainServiceBase<TEntity> _dmn = dmn;

        #endregion

        #region Local methods

        /// <summary>
        /// Validate entity
        /// </summary>
        /// <param name="entity">Entity instance</param>
        protected virtual void ValidateEntity(TEntity entity)
        {
            entity.Validate();
        }

        #endregion

        #region Abstract methods

        /// <summary>
        /// Get entity by key
        /// </summary>
        /// <param name="dto">Dto instance</param>
        /// <param name="cancellationToken">A System.Threading.CancellationToken to observe while waiting for the task to complete</param>
        protected abstract Task<TEntity> GetEntityByKeyAsync(TDto dto, CancellationToken cancellationToken = default);

        /// <summary>
        /// Map dto to entity
        /// </summary>
        /// <param name="dto">Dto instance</param>
        /// <param name="entity">Entity instance</param>
        protected abstract void MapToEntity(TDto dto, TEntity entity);

        /// <summary>
        /// Map dto to entity
        /// </summary>
        /// <param name="dto">Dto instance</param>
        protected abstract TEntity MapToEntity(TDto dto);

        /// <summary>
        /// Map entity to dto
        /// </summary>
        /// <param name="entity">Entity instance</param>
        protected abstract TDto MapToDto(TEntity entity);

        #endregion

        #region Public methods

        ///<inheritdoc/>
        public virtual async Task<TDto> AddAsync(TDto dto, CancellationToken cancellationToken = default)
        {

            bool useTransaction = !_uow.TransactionStarted;

            TEntity entity = MapToEntity(dto);
            ValidateEntity(entity);

            TDto result;
            if (entity.IsValid)
            {
                if (useTransaction) await _uow.BeginTransactionAsync(cancellationToken);

                TEntity dmnResult = await _dmn.AddAsync(entity, cancellationToken);
                await _uow.SaveChangesAsync(cancellationToken);
                result = MapToDto(dmnResult);

                if (useTransaction) await _uow.CommitAsync(cancellationToken);
            }
            else
            {
                result = MapToDto(entity);
            }

            return result;

        }

        ///<inheritdoc/>
        public virtual async Task<TDto> UpdateAsync(int key, TDto dto, CancellationToken cancellationToken = default)
        {

            bool useTransaction = !_uow.TransactionStarted;

            TEntity entity = await GetEntityByKeyAsync(dto, cancellationToken);
            MapToEntity(dto, entity);
            ValidateEntity(entity);

            TDto result;
            if (entity.IsValid)
            {
                if (useTransaction) await _uow.BeginTransactionAsync(cancellationToken);

                TEntity dmnResult = _dmn.Update(key, entity);
                await _uow.SaveChangesAsync(cancellationToken);
                result = MapToDto(dmnResult);

                if (useTransaction) await _uow.CommitAsync(cancellationToken);
            }
            else
            {
                result = MapToDto(entity);
            }

            return result;

        }

        ///<inheritdoc/>
        public virtual async Task<IEnumerable<TDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            IEnumerable<TEntity> dmnResult = await _dmn.GetAllAsync(cancellationToken);
            if (dmnResult.Any()) return Enumerable.Empty<TDto>();
            IEnumerable<TDto> result =
                dmnResult
                    .Select(e => MapToDto(e))
                    .ToList();
            return result;
        }

        ///<inheritdoc/>
        public virtual async Task<TDto?> GetByKeyAsync(int key, CancellationToken cancellationToken = default)
        {
            TEntity? entity = await _dmn.GetByKeyAsync(key, cancellationToken);
            if (entity is null) return default;
            TDto result = MapToDto(entity);
            return result;
        }

        ///<inheritdoc/>
        public virtual async Task DeleteAsync(int key, CancellationToken cancellationToken = default)
        {
            bool useTransaction = !_uow.TransactionStarted;
            if (useTransaction) await _uow.BeginTransactionAsync(cancellationToken);
            _dmn.Delete(key);
            await _uow.SaveChangesAsync(cancellationToken);
            if (useTransaction) await _uow.CommitAsync(cancellationToken);
        }

        #endregion

    }

}
