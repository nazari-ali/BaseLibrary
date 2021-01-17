using BaseLibrary.Exceptions.Helper;
using System;
using System.Net;

namespace BaseLibrary.Exceptions
{
    public partial class UnauthorizedException : SuperException
    {
        public UnauthorizedException() : base(HttpStatusCode.Unauthorized)
        {
            ClientMessage = LocalExceptionMessage.UnauthorizedMessage;
        }

        public UnauthorizedException(object additionalData) : base(HttpStatusCode.Unauthorized, additionalData)
        {
            ClientMessage = LocalExceptionMessage.UnauthorizedMessage;
        }

        public UnauthorizedException(string message) : base(HttpStatusCode.Unauthorized, message)
        {
            ClientMessage = LocalExceptionMessage.UnauthorizedMessage;
        }

        public UnauthorizedException(string message, object additionalData) : base(HttpStatusCode.Unauthorized, message, additionalData)
        {
            ClientMessage = LocalExceptionMessage.UnauthorizedMessage;
        }

        public UnauthorizedException(Exception exception) : base(HttpStatusCode.Unauthorized, exception)
        {
            ClientMessage = LocalExceptionMessage.UnauthorizedMessage;
        }

        public UnauthorizedException(Exception exception, object additionalData) : base(HttpStatusCode.Unauthorized, exception, additionalData)
        {
            ClientMessage = LocalExceptionMessage.UnauthorizedMessage;
        }

        public UnauthorizedException(string message, Exception exception) : base(HttpStatusCode.Unauthorized, message, exception)
        {
            ClientMessage = LocalExceptionMessage.UnauthorizedMessage;
        }

        public UnauthorizedException(string message, Exception exception, object additionalData) : base(HttpStatusCode.Unauthorized, message, exception, additionalData)
        {
            ClientMessage = LocalExceptionMessage.UnauthorizedMessage;
        }
    }
}