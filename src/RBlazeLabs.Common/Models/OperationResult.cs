using RBlazeLabs.Common.Notifications;

namespace RBlazeLabs.Common.Models
{

    /// <summary>
    /// Oeration model result
    /// </summary>
    public class OperationResult<TObject>
    {

        #region Constructors

        /// <summary>
        /// Create a new <see cref=OperationResult"/> sucess instance
        /// </summary>
        /// <param name="result">Object result instance/value</param>
        public OperationResult(TObject result) 
            => Result = result;

        /// <summary>
        /// Create a new <see cref=OperationResult"/> fail instance
        /// </summary>
        /// <param name="notifications">Notification error list</param>
        public OperationResult(IEnumerable<Notification> notifications) 
            => Notifications = notifications;

        /// <summary>
        /// Create a new <see cref=OperationResult"/> exception instance
        /// </summary>
        /// <param name="ex">Exception thrown</param>
        public OperationResult(Exception ex)
            => Exception = ex;

        #endregion

        #region Properties

        /// <summary>
        /// Indicates whether the operation was successful
        /// </summary>
        public bool Success => !Notifications.Any();

        /// <summary>
        /// Object Data result obtained from operation
        /// </summary>
        public TObject? Result { get; private set; }

        /// <summary>
        /// List of errors/validation reviews
        /// </summary>
        public IEnumerable<Notification> Notifications { get; private set; } = Enumerable.Empty<Notification>();

        /// <summary>
        /// Exception thrown when operation was executed
        /// </summary>
        public Exception? Exception { get; private set; }

        /// <summary>
        /// Returns notification messages or thrown exception message
        /// </summary>
        public string SimpleErrorMessage
        {
            get
            {

                string result = "";

                if (!Success)
                {
                    if (Notifications.Any())
                        result = string.Join("|", Notifications.Select(s => $"{s.Key}-{s.Message}"));
                    else if (Exception is not null)
                        result = Exception.GetBaseException().Message;
                }

                return result;

            }
        }

        #endregion

    }

}
