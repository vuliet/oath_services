using System;
using System.Net;

namespace Auth.Application.Exceptions
{
    public class AppException : Exception
    {
        public HttpStatusCode StatusCode { get; }

        public AppException()
            : base()
        {
            StatusCode = HttpStatusCode.BadRequest;
        }

        public AppException(HttpStatusCode statusCode)
            : base()
        {
            StatusCode = statusCode;
        }

        public AppException(string message, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
            : base(message)
        {
            StatusCode = statusCode;
        }

        public AppException(string message, Exception innerException, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
            : base(message, innerException)
        {
            StatusCode = statusCode;
        }
    }
}
