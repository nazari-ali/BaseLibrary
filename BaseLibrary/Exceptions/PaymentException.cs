using System;
using System.Net;

namespace BaseLibrary.Exceptions
{
    public partial class PaymentException : SuperException
    {
        private readonly string _paymentMessage = "";

        public PaymentException() : base(HttpStatusCode.PaymentRequired)
        {
            ClientMessage = _paymentMessage;
        }

        public PaymentException(object additionalData) : base(HttpStatusCode.PaymentRequired, additionalData)
        {
            ClientMessage = _paymentMessage;
        }

        public PaymentException(string message) : base(HttpStatusCode.PaymentRequired, message)
        {
            ClientMessage = _paymentMessage;
        }

        public PaymentException(string message, object additionalData) : base(HttpStatusCode.PaymentRequired, message, additionalData)
        {
            ClientMessage = _paymentMessage;
        }

        public PaymentException(Exception exception) : base(HttpStatusCode.PaymentRequired, exception)
        {
            ClientMessage = _paymentMessage;
        }

        public PaymentException(Exception exception, object additionalData) : base(HttpStatusCode.PaymentRequired, exception, additionalData)
        {
            ClientMessage = _paymentMessage;
        }

        public PaymentException(string message, Exception exception) : base(HttpStatusCode.PaymentRequired, message, exception)
        {
            ClientMessage = _paymentMessage;
        }

        public PaymentException(string message, Exception exception, object additionalData) : base(HttpStatusCode.PaymentRequired, message, exception, additionalData)
        {
            ClientMessage = _paymentMessage;
        }
    }
}