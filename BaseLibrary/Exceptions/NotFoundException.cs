using System;
using System.Net;

namespace BaseLibrary.Exceptions
{
    public partial class NotFoundException : SuperException
    {
        private readonly string _notFoundMessage = "";

        public NotFoundException() : base(HttpStatusCode.NotFound)
        {
            ClientMessage = _notFoundMessage;
        }

        public NotFoundException(object additionalData) : base(HttpStatusCode.NotFound, additionalData)
        {
            ClientMessage = _notFoundMessage;
        }

        public NotFoundException(string message) : base(HttpStatusCode.NotFound, message)
        {
            ClientMessage = _notFoundMessage;
        }

        public NotFoundException(string message, object additionalData) : base(HttpStatusCode.NotFound, message, additionalData)
        {
            ClientMessage = _notFoundMessage;
        }

        public NotFoundException(Exception exception ) : base(HttpStatusCode.NotFound, exception)
        {
            ClientMessage = _notFoundMessage;
        }

        public NotFoundException(Exception exception, object additionalData) : base(HttpStatusCode.NotFound, exception, additionalData)
        {
            ClientMessage = _notFoundMessage;
        }

        public NotFoundException(string message, Exception exception) :  base(HttpStatusCode.NotFound, message, exception)
        {
            ClientMessage = _notFoundMessage;
        }

        public NotFoundException(string message, Exception exception, object additionalData) : base(HttpStatusCode.NotFound, message, exception, additionalData)
        {
            ClientMessage = _notFoundMessage;
        }
    }
}