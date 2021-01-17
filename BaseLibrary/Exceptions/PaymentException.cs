using BaseLibrary.Exceptions.Helper;
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
            ClientMessage = LocalExceptionMessage.PaymentMessage;
        }

        public PaymentException(string message, object additionalData) : base(HttpStatusCode.PaymentRequired, message, additionalData)
        {
            ClientMessage = LocalExceptionMessage.PaymentMessage;
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
            ClientMessage = LocalExceptionMessage.PaymentMessage;
        }

        public PaymentException(string message, Exception exception, object additionalData) : base(HttpStatusCode.PaymentRequired, message, exception, additionalData)
        {
            ClientMessage = LocalExceptionMessage.PaymentMessage;
        }
    }
}