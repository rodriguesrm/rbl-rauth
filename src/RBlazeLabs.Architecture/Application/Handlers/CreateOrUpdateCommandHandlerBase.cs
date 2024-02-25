using MediatR;
using Microsoft.Extensions.Logging;
using RBlazeLabs.Common.Models;
using RBlazeLabs.Architecture.Domain.Entities;

namespace RBlazeLabs.Architecture.Application.Handlers
{

    /// <summary>
    /// Create or update entity command abstract handler
    /// </summary>
    /// <typeparam name="TCreateOrUpdateCommand">Create or update command type</typeparam>
    /// <typeparam name="TResult">Result type</typeparam>
    /// <typeparam name="TEntity">Entity type</typeparam>
    /// <remarks>
    /// Create a new handler instance
    /// </remarks>
    /// <param name="logger">Logger object</param>
    public abstract class CreateOrUpdateCommandHandlerBase<TCreateOrUpdateCommand, TResult, TEntity>(ILogger logger)
        where TCreateOrUpdateCommand : IRequest<OperationResult<TResult>>
        where TEntity : EntityBase<TEntity>
    {

        #region Local objects/variables

        private readonly ILogger _logger = logger;

        #endregion

        #region Abstracts methods

        /// <summary>
        /// Get entity by key
        /// </summary>
        /// <param name="request">Request command data</param>
        /// <param name="cancellationToken">Cancellation token</param>
        protected abstract Task<TEntity> GetEntityByKeyAsync(TCreateOrUpdateCommand request, CancellationToken cancellationToken);

        /// <summary>
        /// Prepare entity to create mapping data
        /// </summary>
        /// <param name="request">Rquest command data</param>
        /// <param name="entity">Entity instance</param>
        /// <param name="isUpdate">Operation status flag (true=Update/false=Create)</param>
        protected abstract TEntity PrepareEntity(TCreateOrUpdateCommand request, TEntity entity, bool isUpdate);

        /// <summary>
        /// Perform save (update) entity in context
        /// </summary>
        /// <param name="entity">Entity to save</param>
        /// <param name="isUpdate">Operation status flag (true=Update/false=Create)</param>
        /// <param name="cancellationToken">Cancellation token</param>
        protected abstract Task<TResult> SaveAsync(TEntity entity, bool isUpdate, CancellationToken cancellationToken);

        #endregion

        #region Handlers

        /// <summary>
        /// Command handler
        /// </summary>
        /// <param name="request">Request command data</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <param name="additionalValidationsAction">Additional validations to be applied to the entity</param>
        protected virtual async Task<OperationResult<TResult>> RunHandler
        (
            TCreateOrUpdateCommand request,
            CancellationToken cancellationToken,
            Action<TCreateOrUpdateCommand, TEntity>? additionalValidationsAction = null
        )
        {
            _logger.LogInformation("{HandlerName} START", GetType().Name);
            OperationResult<TResult> result;

            TEntity? entity = await GetEntityByKeyAsync(request, cancellationToken);
            bool isUpdate = entity is not null;
            entity ??= Activator.CreateInstance<TEntity>();
            entity = PrepareEntity(request, entity, isUpdate);
            entity.Validate();
            additionalValidationsAction?.Invoke(request, entity);
            if (entity.IsValid)
                result = new OperationResult<TResult>(await SaveAsync(entity, isUpdate, cancellationToken));
            else
                result = new OperationResult<TResult>(entity.Notifications);

            _logger.LogInformation("{HandlerName} END", GetType().Name);
            return result;

        }

        #endregion

    }
}