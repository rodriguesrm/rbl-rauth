using RBlazeLabs.Common.Contracts.Web;
using System.Security.Claims;

namespace RBlazeLabs.Architecture.Web.Models
{

    /// <summary>
    /// Logged user object class
    /// </summary>
    /// <remarks>
    /// Create a new LoggedUser instance
    /// </remarks>
    /// <param name="accessor">Http context acessor object</param>
    public class HttpLoggedUser(IHttpContextAccessor accessor) : IHttpLoggedUser, IAuthenticatedUser
    {

        #region Local objects/variables

        private readonly IHttpContextAccessor _accessor = accessor;

        #endregion

        #region Properties

        ///<inheritdoc/>
        public int? Id
        {
            get
            {
                string userId =
                    _accessor
                        .HttpContext?
                        .User
                        .Claims
                        .Where(x => x.Type == ClaimTypes.Sid)
                        .Select(x => x.Value)
                        .FirstOrDefault() ?? string.Empty;

                if (!int.TryParse(userId, out int result))
                    return null;
                return result;

            }
        }

        ///<inheritdoc/>
        public string FirstName 
            => _accessor
                .HttpContext?
                .User
                .Claims
                .Where(x => x.Type == ClaimTypes.Name)
                .Select(x => x.Value)
                .FirstOrDefault() ?? string.Empty;

        ///<inheritdoc/>
        public string LastName 
            => _accessor
                .HttpContext?
                .User
                .Claims
                .Where(x => x.Type == ClaimTypes.Surname)
                .Select(x => x.Value)
                .FirstOrDefault() ?? string.Empty;

        ///<inheritdoc/>
        public string Login 
            => _accessor
                .HttpContext?
                .User
                .Claims
                .Where(x => x.Type == ClaimTypes.NameIdentifier)
                .Select(x => x.Value)
                .FirstOrDefault() ?? string.Empty;

        ///<inheritdoc/>
        public string Email =>
            _accessor
                .HttpContext?
                .User
                .Claims
                .Where(x => x.Type == ClaimTypes.Email)
                .Select(x => x.Value)
                .FirstOrDefault() ?? string.Empty;

        /// <summary>
        /// User scopes
        /// </summary>
        public IEnumerable<string> Scopes =>
            _accessor
            .HttpContext?
            .User
            .Claims
            .Where(x => x.Type == ClaimTypes.GroupSid)
            .Select(x => x.Value) ?? Enumerable.Empty<string>();

        ///<inheritdoc/>
        public IEnumerable<string> Roles =>
            _accessor
                .HttpContext?
                .User
                .Claims
                .Where(x => x.Type == ClaimTypes.Role)
                .Select(x => x.Value) ?? Enumerable.Empty<string>();

        #endregion

    }

}
