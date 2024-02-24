using RBlazeLabs.Architecture.Domain.Entities;

namespace RBlazeLabs.Architecture.Infra.Data
{

    /// <summary>
    /// Generic repository interface
    /// </summary>
    public interface IRepositoryBase<TEntity> : ICommonBase<TEntity>
        where TEntity : EntityBase<TEntity>
    {

    }

}