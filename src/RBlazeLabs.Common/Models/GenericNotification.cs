namespace RBlazeLabs.Common.Models
{

    /// <summary>
    /// Generic notification response model
    /// </summary>
    /// <remarks>
    /// Create a new GenericNotificationResponse instance
    /// </remarks>
    /// <param name="property">Property/Field name</param>
    /// <param name="message">Message detail</param>
    public class GenericNotification(string property, string message)
    {

        #region Constructors

        #endregion

        #region Properties

        /// <summary>
        /// Property/Field name
        /// </summary>
        /// <example>Name</example>
        public string Property { get; set; } = property;

        /// <summary>
        /// Message detail
        /// </summary>
        /// <example>The name field is required</example>
        public string Message { get; set; } = message;

        #endregion

    }

}
