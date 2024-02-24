using RBlazeLabs.Architecture.Domain.Entities;

namespace RBlazeLabs.Architecture.Domain.Services
{

    /// <summary>
    /// Services domain insterface
    /// </summary>
    public interface IDomainServiceBase<TEntity> : ICommonBase<TEntity>
        where TEntity : EntityBase<TEntity>
    {

    }

}
