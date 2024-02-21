using RBlazeLabs.Common.Validations.Constracts;

namespace RBlazeLabs.Common.ValueObjects
{

    /// <summary>
    /// Nullable author value object model
    /// </summary>
    public class AuthorNullable : BaseVO
    {

        #region Constructors

        /// <summary>
        /// Create a new Nullable-Author-Value-Object instance
        /// </summary>
        /// <param name="id">Id key value</param>
        /// <param name="name">Author name</param>
        public AuthorNullable(int id, string name)
        {
            Id = id;
            Name = name;
            Validate();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Author id
        /// </summary>
        public int? Id { get; private set; }

        /// <summary>
        /// Author name
        /// </summary>
        public string? Name { get; private set; }

        #endregion

        #region Overrides

        ///<inheritdoc/>
        protected override void Validate()
        {
            AddNotifications(new SimpleStringValidationContract(Name, nameof(Name), false, 2, 150).Contract.Notifications);
        }

        #endregion

    }
}
