using FluentValidator;

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
        public static IList<Notification> ToGenericNotifications(this IDictionary<string, string> notifications)
            => notifications.Select(d => new Notification(d.Key, d.Value)).ToList();

    }
}
