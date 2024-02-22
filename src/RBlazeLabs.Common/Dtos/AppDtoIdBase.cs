namespace RBlazeLabs.Common.Dtos
{

    /// <summary>
    /// Abstract dto-id model base object
    /// </summary>
    /// <typeparam name="TKey">Entity key type</typeparam>
    public abstract class AppDtoIdBase : AppDtoBase
    {

        #region Properties

        /// <summary>
        /// Entity key
        /// </summary>
        public int Id { get; set; }

        #endregion

    }

}
