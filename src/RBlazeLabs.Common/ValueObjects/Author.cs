using RBlazeLabs.Common.Abstractions;
using RBlazeLabs.Common.Resources;
using RBlazeLabs.Common.Validators;

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
            if (Id <= 0)

                AddNotification(
                    nameof(Id),
                    ServiceActivator.GetStringInLocalizer<SharedLanguageResource>("ID_INVALID", "Id is invalid")
                );

            this.ValidateModel<string, SimpleStringValidator>(Name);

        }

        #endregion

    }
}
