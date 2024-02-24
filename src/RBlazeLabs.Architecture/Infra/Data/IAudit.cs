namespace RBlazeLabs.Architecture.Infra.Data
{

    /// <summary>
    /// Log audit interface
    /// </summary>
    public interface IAudit
    {

        #region Properties

        /// <summary>
        /// Row's create date
        /// </summary>
        DateTime CreatedOn { get; set; }

        /// <summary>
        /// Row's changed date
        /// </summary>
        DateTime? ChangedOn { get; set; }

        /// <summary>
        /// User's id created row
        /// </summary>
        int CreatedBy { get; set; }

        /// <summary>
        /// User's id changed row
        /// </summary>
        int? ChangedBy { get; set; }

        #endregion

    }

}
