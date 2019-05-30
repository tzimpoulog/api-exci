using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;

namespace ExciApi.ExceptionHelper
{
    public class ExciExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            if (context.Exception is ExciException exception)
            {
                var message = "";
                if (exception.InnerException is DbEntityValidationException inner)
                {
                    message = inner.Message;
                }
                context.Response = context.Request.CreateErrorResponse(
                    exception.StatusCode, $"{exception.Message}\n{message}");
            }

            if (context.Exception is DbEntityValidationException dbException)
            {
                var errorMessages = dbException.EntityValidationErrors
                    .SelectMany(x => x.ValidationErrors)
                    .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);
                context.Response = context.Request.CreateErrorResponse(
                HttpStatusCode.InternalServerError, $"{dbException.Message};{fullErrorMessage}");
        }

        }
    }
}