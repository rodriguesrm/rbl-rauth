using MediatR;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using RBlazeLabs.Common.Models;
using RBlazeLabs.Architecture.Domain.Entities;
using RBlazeLabs.Common.Abstractions;
using RBlazeLabs.Common.Notifications;

namespace RBlazeLabs.Architecture.Application.Handlers
{

    /// <summary>
    /// Create entity command abstract handler
    /// </summary>
    /// <remarks>
    /// Create a new handler instance
    /// </remarks>
    /// <param name="logger">Logger object</param>
    public abstract class UpdateCommandHandlerBase<TUpdateCommand, TResult, TEntity>(ILogger logger)
        where TUpdateCommand : IRequest<OperationResult<TResult>>
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
        protected abstract Task<TEntity> GetEntityByKeyAsync(TUpdateCommand request, CancellationToken cancellationToken);

        /// <summary>
        /// Prepare entity to create mapping data
        /// </summary>
        /// <param name="request">Rquest command data</param>
        /// <param name="entity">Entity instance</param>
        protected abstract void PrepareEntity(TUpdateCommand request, TEntity entity);

        /// <summary>
        /// Perform save (update) entity in context
        /// </summary>
        /// <param name="entity">Entity to save</param>
        /// <param name="cancellationToken">Cancellation token</param>
        protected abstract Task<TResult> SaveAsync(TEntity entity, CancellationToken cancellationToken);

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
            TUpdateCommand request,
            CancellationToken cancellationToken,
            Action<TUpdateCommand, TEntity>? additionalValidationsAction = null
        )
        {
            _logger.LogInformation("{HandlerName} START", GetType().Name);
            OperationResult<TResult> result;

            TEntity? entity = await GetEntityByKeyAsync(request, cancellationToken);
            if (entity is null)
            {
                IStringLocalizer<SharedResource>? localizer =
                    (ServiceActivator
                        .GetScope()?
                        .ServiceProvider?
                        .GetService<IStringLocalizer<SharedResource>>()
                    )  ?? throw new InvalidOperationException($"Fail to get StringLocalizer from service container");

                result = new OperationResult<TResult>(
                    new List<Notification>() 
                    { 
                        new(nameof(TEntity), localizer["ENTITY_NOTFOUND"]) 
                    });
            }
            else
            {
                PrepareEntity(request, entity);
                entity.Validate();
                additionalValidationsAction?.Invoke(request, entity);
                if (entity.IsValid)
                    result = new OperationResult<TResult>(await SaveAsync(entity, cancellationToken));
                else
                    result = new OperationResult<TResult>(entity.Notifications);
            }

            _logger.LogInformation("{HandlerName} END", GetType().Name);
            return result;
        }

        #endregion

    }
}
