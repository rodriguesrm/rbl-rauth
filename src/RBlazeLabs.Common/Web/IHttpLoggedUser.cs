namespace RBlazeLabs.Common.Web
{

    /// <summary>
    /// Http logged application user interface
    /// </summary>
    public interface IHttpLoggedUser
    {

        /// <summary>
        /// User id
        /// </summary>
        int? Id { get; }

        /// <summary>
        /// User first name
        /// </summary>
        string FirstName { get; }

        /// <summary>
        /// User last name
        /// </summary>
        string LastName { get; }

        /// <summary>
        /// User login
        /// </summary>
        string Login { get; }

        /// <summary>
        /// User e-mail
        /// </summary>
        string Email { get; }

        /// <summary>
        /// User scopes
        /// </summary>
        IEnumerable<string> Scopes { get; }

        /// <summary>
        /// User roles
        /// </summary>
        IEnumerable<string> Roles { get; }

    }

}
