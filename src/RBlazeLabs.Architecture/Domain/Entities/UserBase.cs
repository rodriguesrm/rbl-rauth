using RBlazeLabs.Common.Entities;
using RBlazeLabs.Common.ValueObjects;

namespace RBlazeLabs.Architecture.Domain.Entities
{

    /// <summary>
    /// User entity base
    /// </summary>
    /// <typeparam name="TUser">User entity object</typeparam>
    public abstract class UserBase<TUser> : EntityIdBase<TUser>, IActive
        where TUser : UserBase<TUser>
    {

        #region Constructors

        /// <summary>
        /// Create a new user instance
        /// </summary>
        public UserBase() : base() { }

        /// <summary>
        /// Create a new user instance
        /// </summary>
        /// <param name="id">Category id value</param>
        public UserBase(int id) : base(id) { }

        #endregion

        #region Properties

        /// <summary>
        /// User full name
        /// </summary>
        public Name Name { get; set; } = new Name(string.Empty, string.Empty);

        /// <summary>
        /// Indicate if entity is active
        /// </summary>
        public bool IsActive { get; set; }

        #endregion

        #region Public methods

        /// <summary>
        /// Validate entity
        /// </summary>
        public override void Validate()
        {
            //UNDONE: Uncoment this line
            // Name.Validate();
            AddNotifications(Name.Notifications);
        }

        #endregion

    }
}
