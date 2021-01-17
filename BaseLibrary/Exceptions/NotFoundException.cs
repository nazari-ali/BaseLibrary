using BaseLibrary.Exceptions.Helper;
using System;
using System.Net;

namespace BaseLibrary.Exceptions
{
    public partial class NotFoundException : SuperException
    {
        public NotFoundException() : base(HttpStatusCode.NotFound)
        {
            ClientMessage = LocalExceptionMessage.NotFoundMessage;
        }

        public NotFoundException(object additionalData) : base(HttpStatusCode.NotFound, additionalData)
        {
            ClientMessage = LocalExceptionMessage.NotFoundMessage;
        }

        public NotFoundException(string message) : base(HttpStatusCode.NotFound, message)
        {
            ClientMessage = LocalExceptionMessage.NotFoundMessage;
        }

        public NotFoundException(string message, object additionalData) : base(HttpStatusCode.NotFound, message, additionalData)
        {
            ClientMessage = LocalExceptionMessage.NotFoundMessage;
        }

        public NotFoundException(Exception exception ) : base(HttpStatusCode.NotFound, exception)
        {
            ClientMessage = LocalExceptionMessage.NotFoundMessage;
        }

        public NotFoundException(Exception exception, object additionalData) : base(HttpStatusCode.NotFound, exception, additionalData)
        {
            ClientMessage = LocalExceptionMessage.NotFoundMessage;
        }

        public NotFoundException(string message, Exception exception) :  base(HttpStatusCode.NotFound, message, exception)
        {
            ClientMessage = LocalExceptionMessage.NotFoundMessage;
        }

        public NotFoundException(string message, Exception exception, object additionalData) : base(HttpStatusCode.NotFound, message, exception, additionalData)
        {
            ClientMessage = LocalExceptionMessage.NotFoundMessage;
        }
    }
}