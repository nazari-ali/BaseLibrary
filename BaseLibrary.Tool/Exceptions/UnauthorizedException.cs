using BaseLibrary.Tool.Exceptions.Helper;
using BaseLibrary.Tool.Extensions;
using System;
using System.Net;

namespace BaseLibrary.Tool.Exceptions
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
            if (!message.HasValue())
                ClientMessage = LocalExceptionMessage.UnauthorizedMessage;

            ClientMessage = message;
        }

        public UnauthorizedException(string message, object additionalData) : base(HttpStatusCode.Unauthorized, message, additionalData)
        {
            if (!message.HasValue())
                ClientMessage = LocalExceptionMessage.UnauthorizedMessage;

            ClientMessage = message;
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
            if (!message.HasValue())
                ClientMessage = LocalExceptionMessage.UnauthorizedMessage;

            ClientMessage = message;
        }

        public UnauthorizedException(string message, Exception exception, object additionalData) : base(HttpStatusCode.Unauthorized, message, exception, additionalData)
        {
            if (!message.HasValue())
                ClientMessage = LocalExceptionMessage.UnauthorizedMessage;

            ClientMessage = message;
        }
    }
}