using BaseLibrary.Exceptions.Helper;
using BaseLibrary.Extensions;
using System;
using System.Net;

namespace BaseLibrary.Exceptions
{
    public partial class PaymentException : SuperException
    {
        public PaymentException() : base(HttpStatusCode.PaymentRequired)
        {
            ClientMessage = LocalExceptionMessage.PaymentMessage;
        }

        public PaymentException(object additionalData) : base(HttpStatusCode.PaymentRequired, additionalData)
        {
            ClientMessage = LocalExceptionMessage.PaymentMessage;
        }

        public PaymentException(string message) : base(HttpStatusCode.PaymentRequired, message)
        {
            if (!message.HasValue())
                ClientMessage = LocalExceptionMessage.PaymentMessage;

            ClientMessage = message;
        }

        public PaymentException(string message, object additionalData) : base(HttpStatusCode.PaymentRequired, message, additionalData)
        {
            if (!message.HasValue())
                ClientMessage = LocalExceptionMessage.PaymentMessage;

            ClientMessage = message;
        }

        public PaymentException(Exception exception) : base(HttpStatusCode.PaymentRequired, exception)
        {
            ClientMessage = LocalExceptionMessage.PaymentMessage;
        }

        public PaymentException(Exception exception, object additionalData) : base(HttpStatusCode.PaymentRequired, exception, additionalData)
        {
            ClientMessage = LocalExceptionMessage.PaymentMessage;
        }

        public PaymentException(string message, Exception exception) : base(HttpStatusCode.PaymentRequired, message, exception)
        {
            if (!message.HasValue())
                ClientMessage = LocalExceptionMessage.PaymentMessage;

            ClientMessage = message;
        }

        public PaymentException(string message, Exception exception, object additionalData) : base(HttpStatusCode.PaymentRequired, message, exception, additionalData)
        {
            if (!message.HasValue())
                ClientMessage = LocalExceptionMessage.PaymentMessage;

            ClientMessage = message;
        }
    }
}