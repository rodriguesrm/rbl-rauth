using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace RBlazeLabs.Architecture.Web.Filters
{

    /// <summary>
    /// Add required header parameter in swagger user-interface
    /// </summary>
    /// <param name="configuration">Configuration object</param>
    public class AddAppKeyHeaderParameter(IConfiguration configuration) : IOperationFilter
    {

        #region Local objects/variables

        private readonly string _appKey = configuration["Scope:Key"] ?? string.Empty;
#pragma warning disable IDE0052 // Remove unread private members
        private readonly string _appAccess = configuration["Scope:Access"] ?? string.Empty;
#pragma warning restore IDE0052 // Remove unread private members
        private readonly bool _isProd = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == Environments.Production;

        #endregion

        #region Public methods

        ///<inheritdoc/>
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            operation.Parameters ??= new List<OpenApiParameter>();

            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "app-key",
                Description = "Application key id",
                In = ParameterLocation.Header,
                Required = false,
                Schema = new OpenApiSchema
                {
                    Type = "String",
                    Default = !_isProd ? new OpenApiString(_appKey) : null
                },

            });

            #endregion

        }
    }

}
