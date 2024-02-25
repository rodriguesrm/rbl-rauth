﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using RBlazeLabs.Common.Notifications;

namespace RSoft.Lib.Common.Web.Api
{

    /// <summary>
    /// API Controller base
    /// </summary>
    [Produces("application/json")]
    [Authorize]
    public abstract class ApiBaseController : ControllerBase
    {

        #region Local objects/variables

        #endregion

        #region Constructors

        /// <summary>
        /// Create a new controller instance
        /// </summary>
        public ApiBaseController() { }

        #endregion

        #region Local Properties

        /// <summary>
        /// Application id passed in the request header
        /// </summary>
        protected Guid? AppKey
        {
            get
            {
                var headerInfo = Request?.Headers["app-key"];
                if (!Guid.TryParse(headerInfo, out Guid result))
                    return null;
                return result;
            }
        }

        /// <summary>
        /// Accept-Language passed in the request header
        /// </summary>
        protected string AcceptLanguage
            => Request?.Headers["Accept-Language"] ?? string.Empty;

        /// <summary>
        /// Convert a dictionary into a NotificationResponse list
        /// </summary>
        /// <param name="dictionary">Notifications dictionary(string, string)</param>
        protected IEnumerable<Notification> PrepareNotifications(IDictionary<string, string> dictionary)
            => dictionary.Select(itm => new Notification(itm.Key, itm.Value));

        #endregion

        #region Local Methods

        /// <summary>
        /// Handles exception
        /// </summary>
        /// <param name="code">Response http code</param>
        /// <param name="ex">Exception object</param>
        protected IActionResult HandleException(int code, Exception ex)
        {
            string msg = ex.GetBaseException().Message;
            string traceId = Request.HttpContext.TraceIdentifier;
            //return StatusCode(code, new GerericExceptionResponse(code.ToString(), msg, traceId));
            return StatusCode(code, new Notification(code.ToString(), msg));
            //TODO: Get GenericExceptionResponse from RSoft.Logs
        }

        /// <summary>
        /// Performs the base operation and calls the callback function
        /// </summary>
        /// <param name="callback">Callback function operation</param>
        /// <param name="cancellationToken">A System.Threading.CancellationToken to observe while waiting for the task to complete</param>
        protected async Task<IActionResult> RunActionAsync(Task<IActionResult> callback, CancellationToken cancellationToken = default)
            => await RunActionAsync(callback, false, false, cancellationToken);

        /// <summary>
        /// Performs the base operation and calls the callback function
        /// </summary>
        /// <param name="callback">Callback function operation</param>
        /// /// <param name="validateRequest">Indicates whether a request validation should be performed</param>
        /// <param name="cancellationToken">A System.Threading.CancellationToken to observe while waiting for the task to complete</param>
        protected async Task<IActionResult> RunActionAsync(Task<IActionResult> callback, bool validateRequest, CancellationToken cancellationToken = default)
            => await RunActionAsync(callback, validateRequest, false, cancellationToken);

        /// <summary>
        /// Performs the base operation and calls the callback function
        /// </summary>
        /// <param name="callback">Callback function operation</param>
        /// <param name="validateRequest">Indicates whether a request validation should be performed</param>
        /// <param name="catchException">Indicates whether to catch exceptions</param>
        /// <param name="cancellationToken">A System.Threading.CancellationToken to observe while waiting for the task to complete</param>
        protected async Task<IActionResult> RunActionAsync(Task<IActionResult> callback, bool validateRequest, bool catchException, CancellationToken cancellationToken = default)
        {

            try
            {

                if (validateRequest)
                {
                    if (!ModelState.IsValid)
                    {
                        IEnumerable<Notification> msg = GetModelErrors(ModelState);
                        return BadRequest(msg);
                    }
                }

                return await Task.Run(() =>
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    return callback;
                }, cancellationToken);

            }
            catch (Exception ex)
            {

                if (catchException)
                    return HandleException(StatusCodes.Status500InternalServerError, ex);
                else
                    throw;

            }

        }

        /// <summary>
        /// Obtain requisition validation messages from model-request validation
        /// </summary>
        /// <param name="modelState">Dicionário de model-state</param>
        protected IEnumerable<Notification> GetModelErrors(ModelStateDictionary modelState)
        {

            IList<Notification> result = new List<Notification>();

            foreach (var item in modelState)
            {

                string prop = string.IsNullOrWhiteSpace(item.Key) ? "body" : item.Key;
                string errors = string.Join(" | ", item.Value.Errors.ToList().Select(x => x.ErrorMessage).ToList()).Trim();
                if (!string.IsNullOrWhiteSpace(errors))
                    result.Add(new Notification(prop, errors));
            }

            return result;

        }

        /// <summary>
        /// Obtain requisition validation messages from entity-notifications
        /// </summary>
        /// <param name="notifications">Notifications list generated by the domain layer</param>
        protected IEnumerable<Notification> GetNotificationsErrors(IReadOnlyCollection<Notification> notifications)
        {

            IList<Notification> result = new List<Notification>();

            foreach (Notification n in notifications)
            {
                if (!string.IsNullOrWhiteSpace(n.Message))
                    result.Add(new Notification(n.Key, n.Message));
            }

            return result;

        }

        #endregion

    }
}
