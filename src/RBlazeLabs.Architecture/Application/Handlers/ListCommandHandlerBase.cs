using MediatR;
using Microsoft.Extensions.Logging;
using RBlazeLabs.Architecture.Domain.Entities;
using RBlazeLabs.Common.Models;

namespace RBlazeLabs.Architecture.Application.Handlers
{

    /// <summary>
    /// List category command handler
    /// </summary>
    /// <param name="logger">Logger object</param>
    public abstract class ListCommandHandlerBase<TListCommand, TDto, TEntity>(ILogger logger)
        where TListCommand : IRequest<OperationResult<IEnumerable<TDto>>>
        where TEntity : EntityBase<TEntity>
    {

        #region Local objects/variables

        private readonly ILogger _logger = logger;

        #endregion

        #region Abstract methods

        /// <summary>
        /// Get entity by key
        /// </summary>
        /// <param name="request">Command request data</param>
        /// <param name="cancellationToken">Cancellation token</param>
        protected abstract Task<IEnumerable<TEntity>> GetAllAsync(TListCommand request, CancellationToken cancellationToken);

        /// <summary>
        /// Map entity list to result list
        /// </summary>
        /// <param name="entities">Entity list</param>
        protected abstract IEnumerable<TDto> MapEntities(IEnumerable<TEntity> entities);

        #endregion

        #region Handlers

        /// <summary>
        /// Command hanlder
        /// </summary>
        /// <param name="request">Command request data</param>
        /// <param name="cancellationToken">Cancellation token</param>
        public async Task<OperationResult<IEnumerable<TDto>>> RunHandler(TListCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("{HandlerName} START", GetType().Name);
            OperationResult<IEnumerable<TDto>> result;
            IEnumerable<TEntity> entities = await GetAllAsync(request, cancellationToken);

            result = entities is null
                ? new OperationResult<IEnumerable<TDto>>(Enumerable.Empty<TDto>())
                : new OperationResult<IEnumerable<TDto>>(MapEntities(entities));

            _logger.LogInformation("{HandlerName} END", GetType().Name);
            return result;
        }

        #endregion
    }

}
