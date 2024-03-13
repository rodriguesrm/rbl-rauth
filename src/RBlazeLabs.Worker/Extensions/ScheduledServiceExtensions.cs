using RBlazeLabs.Worker.Jobs;
using RBlazeLabs.Worker.Schedule;

namespace RBlazeLabs.Worker.Extensions
{

    /// <summary>
    /// Extension for adding jobs to the container service
    /// </summary>
    public static class ScheduledServiceExtensions
    {
        /// <summary>
        /// Adds the job to the run container
        /// </summary>
        /// <typeparam name="TJob">Job type to be performed</typeparam>
        /// <param name="services">Container services collection</param>
        /// <param name="options">Scheduling options</param>
        public static IServiceCollection AddCronJob<TJob>
        (
            this IServiceCollection services, 
            Action<IScheduleConfig<TJob>> options
        ) where TJob : CronJobService<TJob>
        {

            if (options is null)
                throw new ArgumentNullException(nameof(options), @"Provide schedule settings");

            ScheduleConfig<TJob> config = new();

            options.Invoke(config);
            ArgumentException.ThrowIfNullOrWhiteSpace(
                @"Empty or null cron expression is not allowed", nameof(ScheduleConfig<TJob>.CronExpression)
            );

            services.AddSingleton<IScheduleConfig<TJob>>(config);
            services.AddHostedService<TJob>();

            return services;

        }
    }

}
