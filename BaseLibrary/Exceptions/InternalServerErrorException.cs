using BaseLibrary.Exceptions.Helper;
using System;
using System.Net;

namespace BaseLibrary.Exceptions
{
    public partial class InternalServerErrorException : SuperException
    {
        public InternalServerErrorException() : base(HttpStatusCode.InternalServerError)
        {
            ClientMessage = LocalExceptionMessage.InternalServerErrorMessage;
        }

        public InternalServerErrorException(object additionalData) : base(HttpStatusCode.InternalServerError, additionalData)
        {
            ClientMessage = LocalExceptionMessage.InternalServerErrorMessage;
        }

        public InternalServerErrorException(string message) : base(HttpStatusCode.InternalServerError, message)
        {
            ClientMessage = LocalExceptionMessage.InternalServerErrorMessage;
        }

        public InternalServerErrorException(string message, object additionalData) : base(HttpStatusCode.InternalServerError, message, additionalData)
        {
            ClientMessage = LocalExceptionMessage.InternalServerErrorMessage;
        }

        public InternalServerErrorException(Exception exception) : base(HttpStatusCode.InternalServerError, exception)
        {
            ClientMessage = LocalExceptionMessage.InternalServerErrorMessage;
        }

        public InternalServerErrorException(Exception exception, object additionalData) : base(HttpStatusCode.InternalServerError, exception, additionalData)
        {
            ClientMessage = LocalExceptionMessage.InternalServerErrorMessage;
        }

        public InternalServerErrorException(string message, Exception exception) : base(HttpStatusCode.InternalServerError, message, exception)
        {
            ClientMessage = LocalExceptionMessage.InternalServerErrorMessage;
        }

        public InternalServerErrorException(string message, Exception exception, object additionalData) : base(HttpStatusCode.InternalServerError, message, exception, additionalData)
        {
            ClientMessage = LocalExceptionMessage.InternalServerErrorMessage;
        }
    }
}