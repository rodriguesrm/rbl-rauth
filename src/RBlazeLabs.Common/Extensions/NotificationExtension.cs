using FluentValidator;
using RBlazeLabs.Common.Models;

namespace RBlazeLabs.Common.Extensions
{

    /// <summary>
    /// Dictionary extensions
    /// </summary>
    public static class NotificationExtension
    {

        /// <summary>
        /// Convert notifications to generic notification list
        /// </summary>
        /// <param name="notifications">Notifications list</param>
        public static IList<GenericNotification> ToGenericNotifications(this IEnumerable<Notification> notifications)
            => notifications.Select(n => new GenericNotification(n.Property, n.Message)).ToList();

    }
}
