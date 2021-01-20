using BaseLibrary.Tool.Exceptions.Helper;
using BaseLibrary.Tool.Extensions;
using System;
using System.Net;

namespace BaseLibrary.Tool.Exceptions
{
    public partial class NoContentException : SuperException
    {
        public NoContentException() : base(HttpStatusCode.NoContent)
        {
            ClientMessage = LocalExceptionMessage.NotContentMessage;
        }

        public NoContentException(object additionalData) : base(HttpStatusCode.NoContent, additionalData)
        {
            ClientMessage = LocalExceptionMessage.NotContentMessage;
        }

        public NoContentException(string message) : base(HttpStatusCode.NoContent, message)
        {
            if (!message.HasValue())
                ClientMessage = LocalExceptionMessage.NotContentMessage;

            ClientMessage = message;
        }

        public NoContentException(string message, object additionalData) : base(HttpStatusCode.NoContent, message, additionalData)
        {
            if (!message.HasValue())
                ClientMessage = LocalExceptionMessage.NotContentMessage;

            ClientMessage = message;
        }

        public NoContentException(Exception exception) : base(HttpStatusCode.NoContent, exception)
        {
            ClientMessage = LocalExceptionMessage.NotContentMessage;
        }

        public NoContentException(Exception exception, object additionalData) : base(HttpStatusCode.NoContent, exception, additionalData)
        {
            ClientMessage = LocalExceptionMessage.NotContentMessage;
        }

        public NoContentException(string message, Exception exception) : base(HttpStatusCode.NoContent, message, exception)
        {
            if (!message.HasValue())
                ClientMessage = LocalExceptionMessage.NotContentMessage;

            ClientMessage = message;
        }

        public NoContentException(string message, Exception exception, object additionalData) : base(HttpStatusCode.NoContent, message, exception, additionalData)
        {
            if (!message.HasValue())
                ClientMessage = LocalExceptionMessage.NotContentMessage;

            ClientMessage = message;
        }
    }
}