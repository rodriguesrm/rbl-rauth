namespace RBlazeLabs.Architecture.Web.Models.Responses
{

    /// <summary>
    /// Abstract object model-response with id
    /// </summary>
    /// <param name="id">Key value</param>
    public abstract class EntityIdBaseResponse(int id) : EntityBaseResponse
    {

        #region Properties

        /// <summary>
        /// Entity id value
        /// </summary>
        public int Id { get; set; } = id;

        #endregion

    }

}
