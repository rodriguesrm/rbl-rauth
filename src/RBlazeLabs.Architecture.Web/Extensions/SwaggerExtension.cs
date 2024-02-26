using Asp.Versioning.ApiExplorer;
using Microsoft.OpenApi.Models;
using RBlazeLabs.Architecture.Web.Filters;
using RBlazeLabs.Architecture.Web.Options;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace RBlazeLabs.Architecture.Web.Extensions
{

    /// <summary>
    /// Provides extension methods for Swagger Documentation
    /// </summary>
    public static class SwaggerExtension
    {

        /// <summary>
        /// Adds the Swagger Documentation generator
        /// </summary>
        /// <param name="services">Service collection object instance</param>
        /// <param name="configuration">Configuration object instance</param>
        /// <param name="assemblyName">API application assembly name</param>
        public static IServiceCollection AddSwaggerGenerator
        (
            this IServiceCollection services, 
            IConfiguration configuration, string assemblyName
        )
        {
            string[] versions = ["1.0"];
            return services.AddSwaggerGenerator(configuration, assemblyName, true, versions);
        }

        /// <summary>
        /// Adds the Swagger Documentation generator
        /// </summary>
        /// <param name="services">Service collection object instance</param>
        /// <param name="configuration">Configuration object instance</param>
        /// <param name="assemblyName">API application assembly name</param>
        /// <param name="useAppKeyScope">Indicates if the application is outside the RSoft ecosystem.</param>
        public static IServiceCollection AddSwaggerGenerator
        (
            this IServiceCollection services, 
            IConfiguration configuration, 
            string assemblyName, 
            bool useAppKeyScope
        )
        {
            string[] versions = ["1.0"];
            return services.AddSwaggerGenerator(configuration, assemblyName, useAppKeyScope, versions);
        }

        /// <summary>
        /// Adds the Swagger Documentation generator
        /// </summary>
        /// <param name="services">Service collection object instance</param>
        /// <param name="configuration">Configuration object instance</param>
        /// <param name="assemblyName">API application assembly name</param>
        /// <param name="useAppKeyScope">Indicates if the application is outside the RSoft ecosystem.</param>
        /// <param name="versions">Array list API versions</param>
        public static IServiceCollection AddSwaggerGenerator
        (
            this IServiceCollection services, 
            IConfiguration configuration, 
            string assemblyName, 
            bool useAppKeyScope, 
            params string[] versions
        )
        {

            bool isProd = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == Environments.Production;

            services.Configure<SwaggerOptions>(options => configuration.GetSection("Swagger").Bind(options));

            SwaggerOptions swaggerOptions = new();
            configuration.GetSection("Swagger").Bind(swaggerOptions);

            swaggerOptions.Title = !string.IsNullOrWhiteSpace(swaggerOptions.Title) 
                ? swaggerOptions.Title 
                : "API Title (see documentation)";
            swaggerOptions.Description = !string.IsNullOrWhiteSpace(swaggerOptions.Description) 
                ? swaggerOptions.Description 
                : "API Description (see documentation)";
            swaggerOptions.Contact = !string.IsNullOrWhiteSpace(swaggerOptions.Contact) 
                ? swaggerOptions.Contact 
                : "API Contact (see documentation)";
            swaggerOptions.Uri = !string.IsNullOrWhiteSpace(swaggerOptions.Uri) 
                ? swaggerOptions.Uri 
                : "http://api.uri.see.documentation";

            //TODO: Review
            //services.AddVersionedApiExplorer(p =>
            //{
            //    p.GroupNameFormat = @"'v'VVVV";
            //    p.SubstituteApiVersionInUrl = false;
            //});

            services.AddSwaggerGen(c =>
            {

                c.DocumentFilter<ReplaceVersionWithExactValueInPathFilter>();
                c.OperationFilter<RemoveVersionParameterFilter>();
                if (useAppKeyScope)
                    c.OperationFilter<AddAppKeyHeaderParameter>();
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
                c.EnableAnnotations();

                foreach (var version in versions)
                {
                    c.SwaggerDoc($"v{version}",
                    new OpenApiInfo
                    {
                        Title = swaggerOptions.Title,
                        Version = $"v{version}",
                        Description = swaggerOptions.Description,
                        Contact = new OpenApiContact
                        {
                            Name = swaggerOptions.Contact,
                            Url = new Uri(swaggerOptions.Uri)
                        }
                    });
                }


                if (!isProd)
                {

                    if (swaggerOptions.EnableTryOut && swaggerOptions.EnableJwtTokenAuthentication)
                    {
                        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                        {
                            Description = @"JWT Authorization header using the Bearer scheme. 
                                    <br>Enter 'Bearer' [space] and then your token in the text input below.
                                    <br>Example: 'Bearer 12345abcdef'",
                            Name = "Authorization",
                            In = ParameterLocation.Header,
                            Type = SecuritySchemeType.ApiKey,
                            Scheme = "Bearer"
                        });

                        c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                    {
                        {
                        new OpenApiSecurityScheme
                          {
                            Reference = new OpenApiReference
                              {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                              },
                              Scheme = "oauth2",
                              Name = "Bearer",
                              In = ParameterLocation.Header,

                            },
                            new List<string>()
                        }
                    });
                    }

                }

                var xmlFile = $"{assemblyName}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);

            });

            return services;

        }

        /// <summary>
        /// Enables the use of Swagger Documentation in a web interface
        /// </summary>
        /// <param name="app">IApplicationBuilder object instance</param>
        /// <param name="provider">IApiVersionDescriptionProvider object instance</param>
        public static IApplicationBuilder UseSwaggerDocUI
        (
            this IApplicationBuilder app, 
            IApiVersionDescriptionProvider provider
        )
        {

            bool isProd = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == Environments.Production;

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.RoutePrefix = string.Empty;

                foreach (var description in provider.ApiVersionDescriptions)
                {
                    options.SwaggerEndpoint
                    (
                        $"/swagger/{description.GroupName}/swagger.json",
                        description.GroupName.ToLowerInvariant()
                    );
                    if (isProd)
                        options.SupportedSubmitMethods([]);
                }

                options.DocExpansion(DocExpansion.List);

            });

            return app;
        }

    }

}
