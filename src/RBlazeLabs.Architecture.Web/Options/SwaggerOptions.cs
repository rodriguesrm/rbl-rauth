namespace RBlazeLabs.Architecture.Web.Options
{

    /// <summary>
    /// Swagger options configuration model
    /// </summary>
    public class SwaggerOptions
    {

        #region Properties

        /// <summary>
        /// API title
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// API description
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// API contact support
        /// </summary>
        public string Contact { get; set; } = string.Empty;

        /// <summary>
        /// API contact uri
        /// </summary>
        public string Uri { get; set; } = string.Empty;

        /// <summary>
        /// Indicates whether Swagger's 'TryOut' should be active
        /// </summary>
        public bool EnableTryOut { get; set; }

        /// <summary>
        /// Indicates whether Jwt Token Authentication should be active
        /// </summary>
        public bool EnableJwtTokenAuthentication { get; set; }

        #endregion

    }

}
