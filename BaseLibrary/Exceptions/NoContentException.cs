using System;
using System.Net;

namespace BaseLibrary.Exceptions
{
    public partial class NoContentException : SuperException
    {
        private readonly string _noContentMessage = "";

        public NoContentException() : base(HttpStatusCode.NoContent)
        {
            ClientMessage = _noContentMessage;
        }

        public NoContentException(object additionalData) : base(HttpStatusCode.NoContent, additionalData)
        {
            ClientMessage = _noContentMessage;
        }

        public NoContentException(string message) : base(HttpStatusCode.NoContent, message)
        {
            ClientMessage = _noContentMessage;
        }

        public NoContentException(string message, object additionalData) : base(HttpStatusCode.NoContent, message, additionalData)
        {
            ClientMessage = _noContentMessage;
        }

        public NoContentException(Exception exception) : base(HttpStatusCode.NoContent, exception)
        {
            ClientMessage = _noContentMessage;
        }

        public NoContentException(Exception exception, object additionalData) : base(HttpStatusCode.NoContent, exception, additionalData)
        {
            ClientMessage = _noContentMessage;
        }

        public NoContentException(string message, Exception exception) : base(HttpStatusCode.NoContent, message, exception)
        {
            ClientMessage = _noContentMessage;
        }

        public NoContentException(string message, Exception exception, object additionalData) : base(HttpStatusCode.NoContent, message, exception, additionalData)
        {
            ClientMessage = _noContentMessage;
        }
    }
}