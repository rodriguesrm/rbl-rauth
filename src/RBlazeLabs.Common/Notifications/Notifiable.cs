namespace RBlazeLabs.Common.Notifications
{

    /// <summary>
    /// Abstract class for create a notifiable object
    /// </summary>
    public abstract class Notifiable
    {

        #region Local objects/variables

        private readonly List<Notification> _notifications;

        #endregion

        #region Constructors

        /// <summary>
        /// Create a new object instance
        /// </summary>
        public Notifiable()
        {
            _notifications = [];
        }

        #endregion

        #region Properties

        /// <summary>
        /// Get notifications list (read-only)
        /// </summary>
        public IReadOnlyCollection<Notification> Notifications => _notifications;

        #endregion

        #region Local methods

        /// <summary>
        /// Get a notification instance
        /// </summary>
        /// <param name="key">Notification property key</param>
        /// <param name="message">Notification message content</param>
        private static Notification GetNotificationInstance(string key, string message)
            => new(key, message);

        #endregion

        #region Public methods

        /// <summary>
        /// Add a notification
        /// </summary>
        /// <param name="key">Notification property key</param>
        /// <param name="message">Notification message content</param>
        public void AddNotification(string key, string message)
        {
            var notification = GetNotificationInstance(key, message);
            _notifications.Add(notification);
        }

        /// <summary>
        /// Add a notification
        /// </summary>
        /// <param name="notification">Instance of <see cref="Notification"/></param>
        public void AddNotification(Notification notification)
        {
            _notifications.Add(notification);
        }

        /// <summary>
        /// Add a notification
        /// </summary>
        /// <param name="property">A property type instance</param>
        /// <param name="message">Notification message content</param>
        public void AddNotification(Type property, string message)
        {
            ArgumentNullException.ThrowIfNull(property, nameof(property));
            var notification = GetNotificationInstance(property.Name, message);
            _notifications.Add(notification);
        }

        /// <summary>
        /// Add notifications
        /// </summary>
        /// <param name="notifications"></param>
        public void AddNotifications(IReadOnlyCollection<Notification> notifications)
        {
            _notifications.AddRange(notifications);
        }

        /// <summary>
        /// Add notifications
        /// </summary>
        /// <param name="notifications"></param>
        public void AddNotifications(IList<Notification> notifications)
        {
            _notifications.AddRange(notifications);
        }

        /// <summary>
        /// Add notifications
        /// </summary>
        /// <param name="notifications"></param>
        public void AddNotifications(ICollection<Notification> notifications)
        {
            _notifications.AddRange(notifications);
        }

        /// <summary>
        /// /// Add notifications
        /// </summary>
        /// <param name="item"></param>
        public void AddNotifications(Notifiable item)
        {
            AddNotifications(item.Notifications);
        }

        /// <summary>
        /// Add notifications
        /// </summary>
        /// <param name="items"></param>
        public void AddNotifications(params Notifiable[] items)
        {
            foreach (var item in items)
                AddNotifications(item);
        }

        /// <summary>
        /// Clear all notifications
        /// </summary>
        public void Clear()
        {
            _notifications.Clear();
        }

        /// <summary>
        /// Gets whether the object is valid
        /// </summary>
        public bool IsValid
            => !_notifications.Any();

        #endregion

    }
}
