using RBlazeLabs.Common.Notifications;

namespace RBlazeLabs.Common.Models
{

    /// <summary>
    /// Authenticated result model object
    /// </summary>
    /// <remarks>
    /// Create a new object instance
    /// </remarks>
    /// <param name="success">Indicates whether the operation was successful</param>
    /// <param name="user">Dto da pessoa autenticada ou null se falhou a autenticação</param>
    /// <param name="errors">Dictionary with list of errors/validation reviews</param>
    public class AuthenticateResult<TUserDto> : OperationResult<TUserDto>
        where TUserDto : class
    {

        #region Constructors

        /// <summary>
        /// Create a new <see cref=AuthenticateResult"/> sucess instance
        /// </summary>
        /// <param name="user"><see cref="TUserDto"/> instante with user data</param>
        public AuthenticateResult(TUserDto user) : base(user) { }

        /// <summary>
        /// Create a new <see cref=AuthenticateResult"/> fail instance
        /// </summary>
        /// <param name="notifications">Notification error list</param>
        public AuthenticateResult(IEnumerable<Notification> notifications) : base(notifications)
        { }

        #endregion

        #region Properties

        /// <summary>
        /// Authenticated user data, null if access is denied
        /// </summary>
        public TUserDto? User => Result;

        #endregion
    }

}