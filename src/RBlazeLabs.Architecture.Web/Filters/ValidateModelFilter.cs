using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using RBlazeLabs.Architecture.Web.Models.Responses;

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
            if (!ctx.ModelState.IsValid)
            {

                var errors = ctx
                    .ModelState
                    .Where(x => x.Value is not null && x.Value?.Errors.Count != 0);

                IEnumerable<ResponseNotification> messages = errors
                    .Select(x => 
                        new ResponseNotification
                        (
                            x.Key, 
                            x.Value?.Errors
                                .Select(e => e.ErrorMessage).ToArray()
                        )
                    )
                    .ToList()
                    .Where(e => e.Message.Length != 0);

                BadRequestObjectResult result = new(messages);
                ctx.Result = result;

            }
        }
    }

}
