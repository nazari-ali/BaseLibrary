using BaseLibrary.Tool.Exceptions.Helper;
using BaseLibrary.Tool.Extensions;
using System;
using System.Net;
namespace BaseLibrary.Tool.Exceptions
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
            if (!message.HasValue())
                ClientMessage = LocalExceptionMessage.ForbiddenMessage;

            ClientMessage = message;
        }

        public ForbiddenException(string message, object additionalData) : base(HttpStatusCode.Forbidden, message, additionalData)
        {
            if (!message.HasValue())
                ClientMessage = LocalExceptionMessage.ForbiddenMessage;

            ClientMessage = message;
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
            if (!message.HasValue())
                ClientMessage = LocalExceptionMessage.ForbiddenMessage;

            ClientMessage = message;
        }

        public ForbiddenException(string message, Exception exception, object additionalData) : base(HttpStatusCode.Forbidden, message, exception, additionalData)
        {
            if (!message.HasValue())
                ClientMessage = LocalExceptionMessage.ForbiddenMessage;

            ClientMessage = message;
        }
    }
}