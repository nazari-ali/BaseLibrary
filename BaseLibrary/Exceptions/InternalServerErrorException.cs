using System;
using System.Net;

namespace BaseLibrary.Exceptions
{
    public partial class InternalServerErrorException : SuperException
    {
        private readonly string _internalServerErrorMessage = "";

        public InternalServerErrorException() : base(HttpStatusCode.InternalServerError)
        {
            ClientMessage = _internalServerErrorMessage;
        }

        public InternalServerErrorException(object additionalData) : base(HttpStatusCode.InternalServerError, additionalData)
        {
            ClientMessage = _internalServerErrorMessage;
        }

        public InternalServerErrorException(string message) : base(HttpStatusCode.InternalServerError, message)
        {
            ClientMessage = _internalServerErrorMessage;
        }

        public InternalServerErrorException(string message, object additionalData) : base(HttpStatusCode.InternalServerError, message, additionalData)
        {
            ClientMessage = _internalServerErrorMessage;
        }

        public InternalServerErrorException(Exception exception) : base(HttpStatusCode.InternalServerError, exception)
        {
            ClientMessage = _internalServerErrorMessage;
        }

        public InternalServerErrorException(Exception exception, object additionalData) : base(HttpStatusCode.InternalServerError, exception, additionalData)
        {
            ClientMessage = _internalServerErrorMessage;
        }

        public InternalServerErrorException(string message, Exception exception) : base(HttpStatusCode.InternalServerError, message, exception)
        {
            ClientMessage = _internalServerErrorMessage;
        }

        public InternalServerErrorException(string message, Exception exception, object additionalData) : base(HttpStatusCode.InternalServerError, message, exception, additionalData)
        {
            ClientMessage = _internalServerErrorMessage;
        }
    }
}