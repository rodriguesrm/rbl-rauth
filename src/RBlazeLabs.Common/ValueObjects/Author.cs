using RBlazeLabs.Common.Abstractions;
using RBlazeLabs.Common.Resources;
using RBlazeLabs.Common.Validations.Constracts;

namespace RBlazeLabs.Common.ValueObjects
{

    /// <summary>
    /// Author value object model
    /// </summary>
    public class Author : BaseVO
    {

        #region Constructors

        /// <summary>
        /// Create a new Author-Value-Object instance
        /// </summary>
        /// <param name="id">Id key value</param>
        /// <param name="name">Author name</param>
        public Author(int id, string name)
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
        public int Id { get; private set; }

        /// <summary>
        /// Author name
        /// </summary>
        public string Name { get; private set; }

        #endregion

        #region Overrides

        ///<inheritdoc/>
        protected override void Validate()
        {
            AddNotifications(new RequiredValidationContract<int>(Id, nameof(Id), 
                ServiceActivator.GetStringInLocalizer<SharedLanguageResource>(
                    "ID_REQUIRED", "Id is required")).Contract.Notifications);
            AddNotifications(new SimpleStringValidationContract(Name, nameof(Name), true, 2, 150)
                .Contract.Notifications);
        }

        #endregion

    }
}
