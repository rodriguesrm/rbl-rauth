using RBlazeLabs.Common.Contracts.Dtos;

namespace RBlazeLabs.Architecture.Application.Services
{

    /// <summary>
    /// Interface base de Application
    /// </summary>
    /// <typeparam name="TDto"></typeparam>
    public interface IAppServiceBase<TDto>
        where TDto : IAppDto
    {

        /// <summary>
        /// Add an entity to the context
        /// </summary>
        /// <param name="dto">Dto object instance</param>
        /// <param name="cancellationToken">A System.Threading.CancellationToken to observe while waiting for the task to complete</param>
        Task<TDto> AddAsync(TDto dto, CancellationToken cancellationToken = default);

        /// <summary>
        /// Update entity in context
        /// </summary>
        /// <param name="key">The values of the primary key for the entity to be found</param>
        /// <param name="dto">Instância do dto</param>
        /// <param name="cancellationToken">A System.Threading.CancellationToken to observe while waiting for the task to complete</param>
        Task<TDto> UpdateAsync(int key, TDto dto, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get all rows
        /// </summary>
        /// <param name="cancellationToken">A System.Threading.CancellationToken to observe while waiting for the task to complete</param>
        Task<IEnumerable<TDto>> GetAllAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Get entity registration by array key values
        /// </summary>
        /// <param name="key">The values of the primary key for the entity to be found</param>
        /// <param name="cancellationToken">A System.Threading.CancellationToken to observe while waiting for the task to complete</param>
        Task<TDto?> GetByKeyAsync(int key, CancellationToken cancellationToken = default);

        /// <summary>
        /// Remove entity from context
        /// </summary>
        /// <param name="key">The values of the primary key for the entity to be found</param>
        /// <param name="cancellationToken">A System.Threading.CancellationToken to observe while waiting for the task to complete</param>
        Task DeleteAsync(int key, CancellationToken cancellationToken = default);

    }
}