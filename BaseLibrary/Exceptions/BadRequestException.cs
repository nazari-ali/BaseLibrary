using System;
using System.Net;

namespace BaseLibrary.Exceptions
{
    public partial class BadRequestException : SuperException
    {
        private readonly string _badRequestMessage = "";

        public BadRequestException() : base(HttpStatusCode.BadRequest)
        {
            ClientMessage = _badRequestMessage;
        }

        public BadRequestException(object additionalData) : base(HttpStatusCode.BadRequest, additionalData)
        {
            ClientMessage = _badRequestMessage;
        }

        public BadRequestException(string message) : base(HttpStatusCode.BadRequest, message)
        {
            ClientMessage = _badRequestMessage;
        }

        public BadRequestException(string message, object additionalData) : base(HttpStatusCode.BadRequest, message, additionalData)
        {
            ClientMessage = _badRequestMessage;
        }

        public BadRequestException(Exception exception) : base(HttpStatusCode.BadRequest, exception)
        {
            ClientMessage = _badRequestMessage;
        }

        public BadRequestException(Exception exception, object additionalData) : base(HttpStatusCode.BadRequest, exception, additionalData)
        {
            ClientMessage = _badRequestMessage;
        }

        public BadRequestException(string message, Exception exception) : base(HttpStatusCode.BadRequest, message, exception)
        {
            ClientMessage = _badRequestMessage;
        }

        public BadRequestException(string message, Exception exception, object additionalData) : base(HttpStatusCode.BadRequest, message, exception, additionalData)
        {
            ClientMessage = _badRequestMessage;
        }
    }
}