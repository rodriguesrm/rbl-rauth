using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RBlazeLabs.Common.Options;

namespace RBlazeLabs.Architecture.IoC
{

    /// <summary>
    /// Dependency injection register
    /// </summary>
    public static class DependencyInjection
    {

        private const string DEFAULT_CONNECTION_STRING_NAME = "DbServer";

        /// <summary>
        /// Register dependency injection services
        /// </summary>
        /// <param name="services">Service collection</param>
        /// <param name="configuration">Configuration object</param>
        public static IServiceCollection AddRBlazeLabsRegister<TDbContext>
        (
            this IServiceCollection services, 
            IConfiguration configuration
        ) where TDbContext : DbContext
            => services.AddRBlazeLabsRegister<TDbContext>(configuration, DEFAULT_CONNECTION_STRING_NAME, true);

        /// <summary>
        /// Register dependency injection services
        /// </summary>
        /// <param name="services">Service collection</param>
        /// <param name="configuration">Configuration object</param>
        /// <param name="connectionStringName">Connectino string name</param>
        public static IServiceCollection AddRBlazeLabsRegister<TDbContext>
        (
            this IServiceCollection services, 
            IConfiguration configuration, 
            string connectionStringName
        ) where TDbContext : DbContext
            => services.AddRBlazeLabsRegister<TDbContext>(configuration, connectionStringName, true);

        /// <summary>
        /// Register dependency injection services
        /// </summary>
        /// <param name="services">Service collection</param>
        /// <param name="configuration">Configuration object</param>
        /// <param name="userLazyLoading">Flag indicate use lazy loading proxy</param>
        public static IServiceCollection AddRBlazeLabsRegister<TDbContext>
        (
            this IServiceCollection services,
            IConfiguration configuration, 
            bool userLazyLoading
        ) where TDbContext : DbContext
            => services.AddRBlazeLabsRegister<TDbContext>(configuration, DEFAULT_CONNECTION_STRING_NAME, userLazyLoading);

        /// <summary>
        /// Register dependency injection services
        /// </summary>
        /// <param name="services">Service collection</param>
        /// <param name="configuration">Configuration object</param>
        /// <param name="connectionStringName">Connectino string name</param>
        /// <param name="useLayzLoadingProxy">Flag indicate use lazy loading proxy</param>
        public static IServiceCollection AddRBlazeLabsRegister<TDbContext>
        (
            this IServiceCollection services, 
            IConfiguration configuration, 
            string connectionStringName, 
            bool useLayzLoadingProxy
        ) where TDbContext : DbContext
        {

            #region DbContext

            services.AddDbContext<TDbContext>(opt =>
            {
                opt.UseMySql
                (
                    configuration.GetConnectionString(connectionStringName), 
                    ServerVersion.AutoDetect(configuration.GetConnectionString(connectionStringName))
                );
                opt.UseLazyLoadingProxies(useLayzLoadingProxy);
            });

            #endregion

            #region Options

            services.Configure<AppClientOptions>(options => configuration.GetSection("AppClient").Bind(options));

            #endregion

            #region Services

            //TODO: When Web.Models is done
            //services.AddScoped<IHttpLoggedUser, HttpLoggedUser>();
            //services.AddScoped<IAuthenticatedUser, HttpLoggedUser>();

            services.Configure<ApiBehaviorOptions>(opt =>
            {
                opt.SuppressModelStateInvalidFilter = true;
            });

            #endregion

            return services;

        }

    }

}
