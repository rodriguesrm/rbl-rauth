namespace RBlazeLabs.Messaging.Options
{

    /// <summary>
    /// Messaging options model
    /// </summary>
    public class MessagingOption
    {

        /// <summary>
        /// Message broker server address (host server)
        /// </summary>
        public string ServerAddress { get; set; } = string.Empty;

        /// <summary>
        /// Virtual host name
        /// </summary>
        public string VirtualHost { get; set; } = string.Empty;

        /// <summary>
        /// Username to connect
        /// </summary>
        public string Username { get; set; } = string.Empty;

        /// <summary>
        /// User password to connect
        /// </summary>
        public string Password { get; set; } = string.Empty;

        /// <summary>
        /// Return RabbitMQ Uri
        /// </summary>
        public string Uri()
        {
            string uri = $"{ServerAddress}";
            if (!string.IsNullOrWhiteSpace(VirtualHost))
            {
                if (!uri.EndsWith('/'))
                    uri += '/';
                uri += VirtualHost;
            }
            return uri;
        }

    }

}