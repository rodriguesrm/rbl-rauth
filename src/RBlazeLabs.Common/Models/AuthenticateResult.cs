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
    public class AuthenticateResult<TUserDto>(bool success, TUserDto user, IDictionary<string, string> errors) 
        : SimpleOperationResult(success, errors)
        where TUserDto : class
    {

        #region Constructors

        #endregion

        #region Properties

        /// <summary>
        /// Authenticated user data, null if access is denied
        /// </summary>
        public TUserDto User { get; protected set; } = user;

        #endregion
    }

}