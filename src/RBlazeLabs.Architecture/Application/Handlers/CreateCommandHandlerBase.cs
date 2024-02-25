using MediatR;
using Microsoft.Extensions.Logging;
using RBlazeLabs.Architecture.Domain.Entities;
using RBlazeLabs.Common.Models;

namespace RBlazeLabs.Architecture.Application.Handlers
{

    /// <summary>
    /// Create entity command abstract handler
    /// </summary>
    /// <typeparam name="TCreateCommand"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="logger">Logger object</param>
    public abstract class CreateCommandHandlerBase<TCreateCommand, TResult, TEntity>(ILogger logger)
        where TCreateCommand : IRequest<OperationResult<TResult>>
        where TEntity : EntityBase<TEntity>
    {

        #region Objects/Variables Locals

        private readonly ILogger _logger = logger;

        #endregion

        #region Abstracts methods

        /// <summary>
        /// Prepare entity to create mapping data
        /// </summary>
        /// <param name="request">Rquest command data</param>
        protected abstract TEntity PrepareEntity(TCreateCommand request);

        /// <summary>
        /// Perform save (create) entity in context
        /// </summary>
        /// <param name="entity">Entity to save</param>
        /// <param name="cancellationToken">Cancellation token</param>
        protected abstract Task<TResult> SaveAsync(TEntity entity, CancellationToken cancellationToken);

        #endregion

        #region Public methods

        /// <summary>
        /// Command handler
        /// </summary>
        /// <param name="request">Request command data</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <param name="additionalValidationsAction">Additional validations to be applied to the entity</param>
        protected virtual async Task<OperationResult<TResult>> RunHandler
        (
            TCreateCommand request,
            CancellationToken cancellationToken,
            Action<TCreateCommand, TEntity>? additionalValidationsAction = null
        )
        {
            _logger.LogInformation("{HandlerName} START", GetType().Name);
            OperationResult<TResult> result;

            TEntity entity = PrepareEntity(request);
            entity.Validate();
            additionalValidationsAction?.Invoke(request, entity);
            if (entity.IsValid)
                result = new OperationResult<TResult>(await SaveAsync(entity, cancellationToken));
            else
                result = new OperationResult<TResult>(entity.Notifications);

            _logger.LogInformation("{HandlerName} END", GetType().Name);
            return result;

        }

        #endregion

    }
}
