using System;
using System.Net;

namespace BaseLibrary.Exceptions
{
    public partial class UpgradeRequiredException : SuperException
    {
        private readonly string _upgradeRequiredMessage = "";

        public UpgradeRequiredException() : base(HttpStatusCode.UpgradeRequired)
        {
            ClientMessage = _upgradeRequiredMessage;
        }

        public UpgradeRequiredException(object additionalData) : base(HttpStatusCode.UpgradeRequired, additionalData)
        {
            ClientMessage = _upgradeRequiredMessage;
        }

        public UpgradeRequiredException(string message) : base(HttpStatusCode.UpgradeRequired, message)
        {
            ClientMessage = _upgradeRequiredMessage;
        }

        public UpgradeRequiredException(string message, object additionalData) : base(HttpStatusCode.UpgradeRequired, message, additionalData)
        {
            ClientMessage = _upgradeRequiredMessage;
        }

        public UpgradeRequiredException(Exception exception) : base(HttpStatusCode.UpgradeRequired, exception)
        {
            ClientMessage = _upgradeRequiredMessage;
        }

        public UpgradeRequiredException(Exception exception, object additionalData) : base(HttpStatusCode.UpgradeRequired, exception, additionalData)
        {
            ClientMessage = _upgradeRequiredMessage;
        }

        public UpgradeRequiredException(string message, Exception exception) : base(HttpStatusCode.UpgradeRequired, message, exception)
        {
            ClientMessage = _upgradeRequiredMessage;
        }

        public UpgradeRequiredException(string message, Exception exception, object additionalData) : base(HttpStatusCode.UpgradeRequired, message, exception, additionalData)
        {
            ClientMessage = _upgradeRequiredMessage;
        }
    }
}