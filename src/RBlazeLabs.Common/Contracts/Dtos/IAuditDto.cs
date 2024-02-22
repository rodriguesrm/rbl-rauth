using RBlazeLabs.Common.Models;

namespace RBlazeLabs.Common.Contracts.Dtos
{

    /// <summary>
    /// Audit Dto interface
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public interface IAuditDto<TKey>
        where TKey : struct
    {

        /// <summary>
        /// Created author data
        /// </summary>
        AuditAuthor CreatedBy { get; set; }

        /// <summary>
        /// Changed author data
        /// </summary>
        AuditAuthor ChangedBy { get; set; }

    }

}
