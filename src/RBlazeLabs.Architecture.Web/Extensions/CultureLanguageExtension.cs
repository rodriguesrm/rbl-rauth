using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;
using RBlazeLabs.Common.Abstractions;
using RBlazeLabs.Common.Options;
using System.Globalization;


namespace RBlazeLabs.Architecture.Web.Extensions
{

    /// <summary>
    /// Provides extension methods to Culture Language
    /// </summary>
    public static class CultureLanguageExtension
    {

        /// <summary>
        /// Add culture-language services
        /// </summary>
        /// <param name="services">Service collection</param>
        /// <param name="configuration">Service configuration</param>
        /// <param name="sectionSettingName">Section name to bind CultureOptions</param>
        public static IServiceCollection AddCultureLanguage
        (
            this IServiceCollection services, 
            IConfiguration configuration, 
            string sectionSettingName = "Application:Culture"
        )
        {

            services.AddLocalization();

            services.Configure<RequestLocalizationOptions>(
                options =>
                {
                    CultureOptions cultureOptions = new();
                    configuration.GetSection(sectionSettingName).Bind(cultureOptions);

                    IList<CultureInfo> supportedCultures = 
                    cultureOptions.SupportedLanguage.Select(c => new CultureInfo(c)).ToList();

                    options.DefaultRequestCulture = 
                        new RequestCulture(
                            culture: cultureOptions.DefaultLanguage, 
                            uiCulture: cultureOptions.DefaultLanguage
                        );
                    options.SupportedCultures = supportedCultures;
                    options.SupportedUICultures = supportedCultures;
                    options.RequestCultureProviders = new[] 
                    { 
                        new HeaderRequestCultureProvider(
                            cultureOptions.DefaultLanguage, 
                            cultureOptions.SupportedLanguage
                        ) 
                    };
                });

            return services;

        }

        /// <summary>
        /// Configure language service
        /// </summary>
        /// <param name="app">IApplicationBuilder object instance</param>
        public static IApplicationBuilder ConfigureLangague(this IApplicationBuilder app)
        {

            ServiceActivator.Configure(app.ApplicationServices);
            IOptions<RequestLocalizationOptions> localizeOptions = 
                app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>()
                ?? throw new InvalidOperationException("Request location options cannot be retrieved");
            app.UseRequestLocalization(localizeOptions.Value);

            return app;
        }

    }

}
