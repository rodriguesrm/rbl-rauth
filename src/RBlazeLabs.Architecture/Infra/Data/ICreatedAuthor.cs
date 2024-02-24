namespace RBlazeLabs.Architecture.Infra.Data
{

    /// <summary>
    /// Created author interface
    /// </summary>
    public interface ICreatedAuthor
    {

        #region Properties

        /// <summary>
        /// Row's create date
        /// </summary>
        DateTime CreatedOn { get; set; }

        /// <summary>
        /// User's id created row
        /// </summary>
        int CreatedBy { get; set; }

        #endregion

    }

}
