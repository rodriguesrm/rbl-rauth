namespace RBlazeLabs.Common.Contracts.Entities
{

    /// <summary>
    /// Domain entity interface
    /// </summary>
    public interface IEntity
    {
        bool Equals(object obj);

        /// <summary>
        /// Get simple domain name
        /// </summary>
        string GetName();

    }
}
