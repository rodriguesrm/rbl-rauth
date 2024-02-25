using MediatR;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RBlazeLabs.Common.Models;
using RBlazeLabs.Architecture.Domain.Entities;
using RBlazeLabs.Common.Notifications;
using RBlazeLabs.Common.Abstractions;

namespace RBlazeLabs.Architecture.Application.Handlers
{

    /// <summary>
    /// Get entity by id command handler abstract base
    /// </summary>
    /// <param name="logger">Logger object</param>
    public abstract class GetByKeyCommandHandlerBase<TGetCommand, TResult, TEntity>(ILogger logger)
        where TGetCommand : IRequest<OperationResult<TResult>>
        where TEntity : EntityBase<TEntity>
    {

        #region Local objects/variables

        private readonly ILogger _logger = logger;

        #endregion

        #region Abstract methods

        /// <summary>
        /// Get entity by key
        /// </summary>
        /// <param name="request">Request command data</param>
        /// <param name="cancellationToken">Cancellation token</param>
        protected abstract Task<TEntity> GetEntityByKeyAsync(TGetCommand request, CancellationToken cancellationToken);

        /// <summary>
        /// Map entity to result
        /// </summary>
        /// <param name="entity"></param>
        protected abstract TResult MapEntity(TEntity entity);

        #endregion

        #region Handlers

        /// <summary>
        /// Command handler
        /// </summary>
        /// <param name="request">Command request data</param>
        /// <param name="cancellationToken">Cancellation token</param>
        public async Task<OperationResult<TResult>> RunHandler(TGetCommand request, CancellationToken cancellationToken)
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
                    ) ?? throw new InvalidOperationException($"Fail to get StringLocalizer from service container");

                result = new OperationResult<TResult>(
                    new List<Notification>()
                    {
                        new(nameof(TEntity), localizer["ENTITY_NOTFOUND"])
                    });

            }
            else
            {
                result = new OperationResult<TResult>(MapEntity(entity));
            }

            _logger.LogInformation("{HandlerName} END", GetType().Name);
            return result;

        }

        #endregion

    }
}
