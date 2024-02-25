using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using RBlazeLabs.Common.Notifications;

namespace RBlazeLabs.Architecture.Web.Filters
{

    /// <summary>
    /// Validation filter of request models
    /// </summary>
    public class ValidateModelFilter : IActionFilter
    {

        ///<inheritdoc/>
        public void OnActionExecuted(ActionExecutedContext ctx) { }

        ///<inheritdoc/>
        public void OnActionExecuting(ActionExecutingContext ctx)
        {
            //TODO: Review
            if (!ctx.ModelState.IsValid)
            {

                IEnumerable<Notification> messages = ctx.ModelState
                    .Where(x => x.Value?.Errors.Count > 0)
                    .ToDictionary
                    (
                        k => k.Key,
                        v => v.Value?.Errors.Select(e => e.ErrorMessage).ToArray()
                    )
                    .Select(itm => new Notification(itm.Key, string.Join('|', itm.Value)))
                    .ToList();

                BadRequestObjectResult result = new(messages);
                ctx.Result = result;

            }
        }
    }

}
