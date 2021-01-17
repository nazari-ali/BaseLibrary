using System;
using System.Net;

namespace BaseLibrary.Exceptions
{
    public partial class UnauthorizedException : SuperException
    {
        private readonly string _unauthorizedMessage = "";

        public UnauthorizedException() : base(HttpStatusCode.Unauthorized)
        {
            ClientMessage = _unauthorizedMessage;
        }

        public UnauthorizedException(object additionalData) : base(HttpStatusCode.Unauthorized, additionalData)
        {
            ClientMessage = _unauthorizedMessage;
        }

        public UnauthorizedException(string message) : base(HttpStatusCode.Unauthorized, message)
        {
            ClientMessage = _unauthorizedMessage;
        }

        public UnauthorizedException(string message, object additionalData) : base(HttpStatusCode.Unauthorized, message, additionalData)
        {
            ClientMessage = _unauthorizedMessage;
        }

        public UnauthorizedException(Exception exception) : base(HttpStatusCode.Unauthorized, exception)
        {
            ClientMessage = _unauthorizedMessage;
        }

        public UnauthorizedException(Exception exception, object additionalData) : base(HttpStatusCode.Unauthorized, exception, additionalData)
        {
            ClientMessage = _unauthorizedMessage;
        }

        public UnauthorizedException(string message, Exception exception) : base(HttpStatusCode.Unauthorized, message, exception)
        {
            ClientMessage = _unauthorizedMessage;
        }

        public UnauthorizedException(string message, Exception exception, object additionalData) : base(HttpStatusCode.Unauthorized, message, exception, additionalData)
        {
            ClientMessage = _unauthorizedMessage;
        }
    }
}