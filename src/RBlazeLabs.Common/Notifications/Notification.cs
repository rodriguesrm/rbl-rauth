namespace RBlazeLabs.Common.Notifications
{

    /// <summary>
    /// Notification template class
    /// </summary>
    /// <param name="key">Notification property key</param>
    /// <param name="message">Notification message content</param>
    public class Notification(string key, string message)
    {

        /// <summary>
        /// Notification property key
        /// </summary>
        public string Key { get; private set; } = key;

        /// <summary>
        /// Notification message content
        /// </summary>
        public string Message { get; private set; } = message;

    }

}