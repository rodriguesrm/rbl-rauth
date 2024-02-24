using FluentValidation;
using FluentValidation.Results;
using RBlazeLabs.Common.Abstractions;
using RBlazeLabs.Common.Notifications;

namespace RBlazeLabs.Common.Validators
{
    public static class ValidatorExtensions
    {

        /// <summary>
        /// Get message from string localizer
        /// </summary>
        /// <typeparam name="TResource">Resource type</typeparam>
        /// <param name="textToLocalizer"></param>
        /// <param name="defaultText"></param>
        /// <param name="replaceArgs">An object array that contains zero or more objects to format.</param>
        public static string GetMessage<TResource>
        (
            this object _, 
            string textToLocalizer, 
            string defaultText,
            params object[]? replaceArgs
        )
            => ServiceActivator.GetStringInLocalizer<TResource>
            (
                textToLocalizer,
                defaultText,
                replaceArgs
            );


        /// <summary>
        /// Performs object validation and transform validation result in notifications
        /// </summary>
        /// <typeparam name="TModel">Model type</typeparam>
        /// <typeparam name="TValidator">Validator object type</typeparam>
        /// <param name="notifiable">Notifiable object to extend</param>
        /// <param name="model">Instance of <typeparamref name="TModel"/> to validate</param>
        public static void ValidateModel<TModel, TValidator>
        (
            this Notifiable notifiable, 
            TModel model, 
            Func<TValidator>? constructor = null
        )
            where TModel : class
            where TValidator : IValidator<TModel>
        {

            TValidator validator = constructor == null
                ? Activator.CreateInstance<TValidator>()
                : constructor.Invoke();

            ValidationResult validationResult = validator.Validate(model);
            IList<Notification> notifications = validationResult
                .Errors.Select(s => new Notification(s.PropertyName, s.ErrorMessage))
                .ToList();
            notifiable.AddNotifications(notifications);

        }

    }
}
