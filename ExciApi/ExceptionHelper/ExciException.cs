using System.Net;

namespace ExciApi.ExceptionHelper
{
    public class ExciException : System.Exception
    {
        public ExciException(HttpStatusCode statusCode, string errorCode, string errorDescription) : base($"{errorCode}::{errorDescription}")
        {
            StatusCode = statusCode;
        }

        public ExciException(HttpStatusCode statusCode)
        {
            StatusCode = statusCode;
        }

        public HttpStatusCode StatusCode { get; }
    }
}