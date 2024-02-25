using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;

namespace RBlazeLabs.Common.Abstractions
{

    /// <summary>
    /// Add static service resolver to use when dependencies injection is not available
    /// </summary>
    public class ServiceActivator
    {

        #region Local objects/variables

        private static IServiceProvider? _serviceProvider;

        #endregion

        #region Public methods

        /// <summary>
        /// Configure ServiceActivator with full serviceProvider
        /// </summary>
        /// <param name="serviceProvider">Service DI provider</param>
        public static void Configure(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        /// <summary>
        /// Create a scope where use this ServiceActivator
        /// </summary>
        /// <param name="serviceProvider">Service DI provider</param>
        /// <exception cref="InvalidOperationException">When provider is not setted</exception>
        public static IServiceScope GetScope(IServiceProvider? serviceProvider = null)
        {
            IServiceProvider? provider = serviceProvider ?? _serviceProvider;
            return provider is null
                ? throw new InvalidOperationException("Provider is null")
                : provider
                    .GetRequiredService<IServiceScopeFactory>()
                    .CreateScope();
        }

        /// <summary>
        /// Get text int string-localizer
        /// </summary>
        /// <typeparam name="T">The System.Type to provide strings for</typeparam>
        /// <param name="textToLocalizer">Text do localizer</param>
        /// <param name="defaultText">Default text when not found in localizer</param>
        /// <returns>String founded or default text is not found or provider is not avaliable</returns>
        public static string GetStringInLocalizer<T>
        (
            string textToLocalizer,
            string defaultText
        ) => GetStringInLocalizer<T>(textToLocalizer, defaultText, null, null);

        /// <summary>
        /// Get text int string-localizer
        /// </summary>
        /// <typeparam name="T">The System.Type to provide strings for</typeparam>
        /// <param name="textToLocalizer">Text do localizer</param>
        /// <param name="defaultText">Default text when not found in localizer</param>
        /// <param name="replaceArgs">An object array that contains zero or more objects to format.</param>
        /// <returns>String founded or default text is not found or provider is not avaliable</returns>
        public static string GetStringInLocalizer<T>
        (
            string textToLocalizer,
            string defaultText,
            params object[]? replaceArgs
        ) => GetStringInLocalizer<T>(textToLocalizer, defaultText, null, replaceArgs);

        /// <summary>
        /// Get text int string-localizer
        /// </summary>
        /// <typeparam name="T">The System.Type to provide strings for</typeparam>
        /// <param name="textToLocalizer">Text do localizer</param>
        /// <param name="defaultText">Default text when not found in localizer</param>
        /// <param name="serviceProvider">Service DI provider</param>
        /// <returns>String founded or default text is not found or provider is not avaliable</returns>
        public static string GetStringInLocalizer<T>
        (
            string textToLocalizer,
            string defaultText,
            IServiceProvider serviceProvider
        ) => GetStringInLocalizer<T>(textToLocalizer, defaultText, serviceProvider, null);

        /// <summary>
        /// Get text int string-localizer
        /// </summary>
        /// <typeparam name="T">The System.Type to provide strings for</typeparam>
        /// <param name="textToLocalizer">Text do localizer</param>
        /// <param name="defaultText">Default text when not found in localizer</param>
        /// <param name="serviceProvider">Service DI provider</param>
        /// <param name="replaceArgs">An object array that contains zero or more objects to format.</param>
        /// <returns>String founded or default text is not found or provider is not avaliable</returns>
        public static string GetStringInLocalizer<T>
        (
            string textToLocalizer,
            string defaultText,
            IServiceProvider? serviceProvider,
            params object[]? replaceArgs
        )
        {

            IServiceProvider? provider = serviceProvider ?? _serviceProvider;

            if (provider is not null)
            {
                IStringLocalizer<T>? localizer = GetScope()?.ServiceProvider.GetService<IStringLocalizer<T>>();
                if (localizer is null)
                    return defaultText;
                LocalizedString localizeResult = localizer[textToLocalizer];

                if (localizeResult.ResourceNotFound)
                    return defaultText;

                string result = localizeResult.Value;
                if (replaceArgs?.Length > 0)
                {
                    try { result = string.Format(result, replaceArgs); }
                    catch (FormatException) { result = defaultText; }
                    catch { throw; }
                }

                return result;
            }

            return defaultText;

        }

        #endregion

    }

}
