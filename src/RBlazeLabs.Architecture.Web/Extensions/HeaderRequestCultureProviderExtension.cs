using Microsoft.AspNetCore.Localization;

namespace RBlazeLabs.Architecture.Web.Extensions
{

    /// <summary>
    /// Provider for determining the culture information of an Microsoft.AspNetCore.Http.HttpRequest
    /// </summary>
    /// <param name="defaultRequestCulture">System default language</param>
    /// <param name="supportedCultures">List of supported culture</param>
    public class HeaderRequestCultureProvider(string defaultRequestCulture, IEnumerable<string> supportedCultures) 
        : RequestCultureProvider
    {

        #region Properties

        /// <summary>
        /// System default language
        /// </summary>
        public string DefaultRequestCulture { get; private set; } = defaultRequestCulture;

        /// <summary>
        /// List of supported culture
        /// </summary>
        public IEnumerable<string> SupportedCultures { get; private set; } = supportedCultures;

        #endregion

        #region Overrides

        /// <summary>
        /// Determine provider culture result in http context
        /// </summary>
        /// <param name="httpContext">Http context object instance</param>
        public override Task<ProviderCultureResult> DetermineProviderCultureResult(HttpContext httpContext)
        {

            if (httpContext == null)
                throw new ArgumentNullException(nameof(httpContext));

            string uiCulture;
            string culture = uiCulture = 
                !string.IsNullOrWhiteSpace(httpContext.Request.Headers["Accept-Language"]) 
                ? httpContext.Request.Headers["Accept-Language"].ToString() 
                : DefaultRequestCulture;

            if (!SupportedCultures.Contains(culture))
                culture = uiCulture = DefaultRequestCulture;

            ProviderCultureResult providerResultCulture = new(culture, uiCulture);

            return Task.FromResult(providerResultCulture);

        }

        #endregion

    }

}