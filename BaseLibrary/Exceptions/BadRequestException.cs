using BaseLibrary.Exceptions.Helper;
using System;
using System.Net;

namespace BaseLibrary.Exceptions
{
    public partial class BadRequestException : SuperException
    {
        public BadRequestException() : base(HttpStatusCode.BadRequest)
        {
            ClientMessage = LocalExceptionMessage.BadRequestMessage;
        }

        public BadRequestException(object additionalData) : base(HttpStatusCode.BadRequest, additionalData)
        {
            ClientMessage = LocalExceptionMessage.BadRequestMessage;
        }

        public BadRequestException(string message) : base(HttpStatusCode.BadRequest, message)
        {
            ClientMessage = LocalExceptionMessage.BadRequestMessage;
        }

        public BadRequestException(string message, object additionalData) : base(HttpStatusCode.BadRequest, message, additionalData)
        {
            ClientMessage = LocalExceptionMessage.BadRequestMessage;
        }

        public BadRequestException(Exception exception) : base(HttpStatusCode.BadRequest, exception)
        {
            ClientMessage = LocalExceptionMessage.BadRequestMessage;
        }

        public BadRequestException(Exception exception, object additionalData) : base(HttpStatusCode.BadRequest, exception, additionalData)
        {
            ClientMessage = LocalExceptionMessage.BadRequestMessage;
        }

        public BadRequestException(string message, Exception exception) : base(HttpStatusCode.BadRequest, message, exception)
        {
            ClientMessage = LocalExceptionMessage.BadRequestMessage;
        }

        public BadRequestException(string message, Exception exception, object additionalData) : base(HttpStatusCode.BadRequest, message, exception, additionalData)
        {
            ClientMessage = LocalExceptionMessage.BadRequestMessage;
        }
    }
}