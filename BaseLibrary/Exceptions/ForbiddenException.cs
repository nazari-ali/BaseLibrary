using System;
using System.Net;

namespace BaseLibrary.Exceptions
{
    public partial class ForbiddenException : SuperException
    {
        private readonly string _forbiddenMessage = "";

        public ForbiddenException() : base(HttpStatusCode.Forbidden)
        {
            ClientMessage = _forbiddenMessage;
        }

        public ForbiddenException(object additionalData) : base(HttpStatusCode.Forbidden, additionalData)
        {
            ClientMessage = _forbiddenMessage;
        }

        public ForbiddenException(string message) : base(HttpStatusCode.Forbidden, message)
        {
            ClientMessage = _forbiddenMessage;
        }

        public ForbiddenException(string message, object additionalData) : base(HttpStatusCode.Forbidden, message, additionalData)
        {
            ClientMessage = _forbiddenMessage;
        }

        public ForbiddenException(Exception exception) : base(HttpStatusCode.Forbidden, exception)
        {
            ClientMessage = _forbiddenMessage;
        }

        public ForbiddenException(Exception exception, object additionalData) : base(HttpStatusCode.Forbidden, exception, additionalData)
        {
            ClientMessage = _forbiddenMessage;
        }

        public ForbiddenException(string message, Exception exception) : base(HttpStatusCode.Forbidden, message, exception)
        {
            ClientMessage = _forbiddenMessage;
        }

        public ForbiddenException(string message, Exception exception, object additionalData) : base(HttpStatusCode.Forbidden, message, exception, additionalData)
        {
            ClientMessage = _forbiddenMessage;
        }
    }
}