namespace RBlazeLabs.Architecture.Web.Models.Responses
{


    /// <summary>
    /// Abstract response base object
    /// </summary>
    public abstract class EntityBaseResponse
    {

        #region Public methods

        /// <summary>
        /// Get object name
        /// </summary>
        public string GetName() 
            => GetType().Name
                .Split(".")
                .ToList()
                .Last();

        #endregion

    }

}
