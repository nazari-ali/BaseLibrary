using BaseLibrary.Exceptions.Helper;
using System;
using System.Net;

namespace BaseLibrary.Exceptions
{
    public partial class ForbiddenException : SuperException
    {
        public ForbiddenException() : base(HttpStatusCode.Forbidden)
        {
            ClientMessage = LocalExceptionMessage.ForbiddenMessage;
        }

        public ForbiddenException(object additionalData) : base(HttpStatusCode.Forbidden, additionalData)
        {
            ClientMessage = LocalExceptionMessage.ForbiddenMessage;
        }

        public ForbiddenException(string message) : base(HttpStatusCode.Forbidden, message)
        {
            ClientMessage = LocalExceptionMessage.ForbiddenMessage;
        }

        public ForbiddenException(string message, object additionalData) : base(HttpStatusCode.Forbidden, message, additionalData)
        {
            ClientMessage = LocalExceptionMessage.ForbiddenMessage;
        }

        public ForbiddenException(Exception exception) : base(HttpStatusCode.Forbidden, exception)
        {
            ClientMessage = LocalExceptionMessage.ForbiddenMessage;
        }

        public ForbiddenException(Exception exception, object additionalData) : base(HttpStatusCode.Forbidden, exception, additionalData)
        {
            ClientMessage = LocalExceptionMessage.ForbiddenMessage;
        }

        public ForbiddenException(string message, Exception exception) : base(HttpStatusCode.Forbidden, message, exception)
        {
            ClientMessage = LocalExceptionMessage.ForbiddenMessage;
        }

        public ForbiddenException(string message, Exception exception, object additionalData) : base(HttpStatusCode.Forbidden, message, exception, additionalData)
        {
            ClientMessage = LocalExceptionMessage.ForbiddenMessage;
        }
    }
}