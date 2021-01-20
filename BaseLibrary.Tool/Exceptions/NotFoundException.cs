using BaseLibrary.Tool.Exceptions.Helper;
using BaseLibrary.Tool.Extensions;
using System;
using System.Net;

namespace BaseLibrary.Tool.Exceptions
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
            if (!message.HasValue())
                ClientMessage = LocalExceptionMessage.NotFoundMessage;

            ClientMessage = message;
        }

        public NotFoundException(string message, object additionalData) : base(HttpStatusCode.NotFound, message, additionalData)
        {
            if (!message.HasValue())
                ClientMessage = LocalExceptionMessage.NotFoundMessage;

            ClientMessage = message;
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
            if (!message.HasValue())
                ClientMessage = LocalExceptionMessage.NotFoundMessage;

            ClientMessage = message;
        }

        public NotFoundException(string message, Exception exception, object additionalData) : base(HttpStatusCode.NotFound, message, exception, additionalData)
        {
            if (!message.HasValue())
                ClientMessage = LocalExceptionMessage.NotFoundMessage;

            ClientMessage = message;
        }
    }
}