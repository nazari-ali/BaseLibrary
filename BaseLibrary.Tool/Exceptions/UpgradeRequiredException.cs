using BaseLibrary.Tool.Exceptions.Helper;
using BaseLibrary.Tool.Extensions;
using System;
using System.Net;

namespace BaseLibrary.Tool.Exceptions
{
    public partial class UpgradeRequiredException : SuperException
    {
        public UpgradeRequiredException() : base(HttpStatusCode.UpgradeRequired)
        {
            ClientMessage = LocalExceptionMessage.UpgradeRequiredMessage;
        }

        public UpgradeRequiredException(object additionalData) : base(HttpStatusCode.UpgradeRequired, additionalData)
        {
            ClientMessage = LocalExceptionMessage.UpgradeRequiredMessage;
        }

        public UpgradeRequiredException(string message) : base(HttpStatusCode.UpgradeRequired, message)
        {
            if (!message.HasValue())
                ClientMessage = LocalExceptionMessage.UpgradeRequiredMessage;

            ClientMessage = message;
        }

        public UpgradeRequiredException(string message, object additionalData) : base(HttpStatusCode.UpgradeRequired, message, additionalData)
        {
            if (!message.HasValue())
                ClientMessage = LocalExceptionMessage.UpgradeRequiredMessage;

            ClientMessage = message;
        }

        public UpgradeRequiredException(Exception exception) : base(HttpStatusCode.UpgradeRequired, exception)
        {
            ClientMessage = LocalExceptionMessage.UpgradeRequiredMessage;
        }

        public UpgradeRequiredException(Exception exception, object additionalData) : base(HttpStatusCode.UpgradeRequired, exception, additionalData)
        {
            ClientMessage = LocalExceptionMessage.UpgradeRequiredMessage;
        }

        public UpgradeRequiredException(string message, Exception exception) : base(HttpStatusCode.UpgradeRequired, message, exception)
        {
            if (!message.HasValue())
                ClientMessage = LocalExceptionMessage.UpgradeRequiredMessage;

            ClientMessage = message;
        }

        public UpgradeRequiredException(string message, Exception exception, object additionalData) : base(HttpStatusCode.UpgradeRequired, message, exception, additionalData)
        {
            if (!message.HasValue())
                ClientMessage = LocalExceptionMessage.UpgradeRequiredMessage;

            ClientMessage = message;
        }
    }
}