using BaseLibrary.Exceptions.Helper;
using System;
using System.Net;

namespace BaseLibrary.Exceptions
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
            ClientMessage = LocalExceptionMessage.NotContentMessage;
        }

        public NoContentException(string message, object additionalData) : base(HttpStatusCode.NoContent, message, additionalData)
        {
            ClientMessage = LocalExceptionMessage.NotContentMessage;
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
            ClientMessage = LocalExceptionMessage.NotContentMessage;
        }

        public NoContentException(string message, Exception exception, object additionalData) : base(HttpStatusCode.NoContent, message, exception, additionalData)
        {
            ClientMessage = LocalExceptionMessage.NotContentMessage;
        }
    }
}