using Microsoft.Extensions.Logging;

namespace RBlazeLabs.Messaging.Extensions
{

    /// <summary>
    /// Provides log extensions methods
    /// </summary>
    public static class LogExtension
    {

        /// <summary>
        /// Formats and writes an informational log message.
        /// </summary>
        /// <param name="logger">Microsoft.Extensions.Logging.ILogger to write to</param>
        /// <param name="message">Log text message</param>
        /// <param name="messageId">The messageId assigned to the message when it was initially Sent</param>
        /// <param name="messageContent">Adicional content to log</param>
        /// <param name="contentName">Tag content name to log</param>
        public static void LogInformation
        (
            this ILogger logger, 
            string message, 
            Guid messageId, 
            string messageContent, 
            string contentName = "MessageContent"
        )
        {
            IList<KeyValuePair<string, object>> pairs = new List<KeyValuePair<string, object>>
            {
                new("MessageId", messageId),
                new(contentName, messageContent)
            };
            logger.Log(
                LogLevel.Information, 
                new EventId(1010, "RBlazeLabs:Process:Message"), 
                state: pairs, null, (i, e) => { return message; }
            );
        }

        /// <summary>
        /// Formats and writes an informational log message.
        /// </summary>
        /// <param name="logger">Microsoft.Extensions.Logging.ILogger to write to</param>
        /// <param name="message">Log text message</param>
        /// <param name="messageId">The messageId assigned to the message when it was initially Sent</param>
        public static void LogInformation(this ILogger logger, string message, Guid messageId)
        {
            IList<KeyValuePair<string, object>> pairs = new List<KeyValuePair<string, object>>
            {
                new ("MessageId", messageId),
            };
            logger.Log(
                LogLevel.Information, 
                new EventId(1010, "RBlazeLabs:Process:Message"),
                state: pairs, null, (i, e) => { return message; }
            );
        }

    }

}
