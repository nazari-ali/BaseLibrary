using BaseLibrary.Tool.Extensions;
using System;
using System.Net;

namespace BaseLibrary.Tool.Exceptions
{
    public abstract class SuperException : Exception
    {
        public HttpStatusCode HttpStatusCode { get; set; }
        public string SystemMessage { get; set; }
        public string ClientMessage { get; set; }

        public SuperException(HttpStatusCode httpStatusCode) : base()
        {
            HttpStatusCode = httpStatusCode;
            SystemMessage = GetMessage(httpStatusCode);
        }

        public SuperException(HttpStatusCode httpStatusCode, object additionalData) : base()
        {
            HttpStatusCode = httpStatusCode;
            SystemMessage = GetMessage(httpStatusCode, null, null, additionalData);
        }

        public SuperException(HttpStatusCode httpStatusCode, string message) : base(message)
        {
            HttpStatusCode = httpStatusCode;
            SystemMessage = GetMessage(httpStatusCode, message);
        }

        public SuperException(HttpStatusCode httpStatusCode, string message, object additionalData) : base(message)
        {
            HttpStatusCode = httpStatusCode;
            SystemMessage = GetMessage(httpStatusCode, message, null, additionalData);
        }

        public SuperException(HttpStatusCode httpStatusCode, Exception exception) : base(null, exception)
        {
            HttpStatusCode = httpStatusCode;
            SystemMessage = GetMessage(httpStatusCode, null, exception);
        }

        public SuperException(HttpStatusCode httpStatusCode, Exception exception, object additionalData) : base(null, exception)
        {
            HttpStatusCode = httpStatusCode;
            SystemMessage = GetMessage(httpStatusCode, null, exception, additionalData);
        }

        public SuperException(HttpStatusCode httpStatusCode, string message, Exception exception) : base(message, exception)
        {
            HttpStatusCode = httpStatusCode;
            SystemMessage = GetMessage(httpStatusCode, message, exception);
        }

        public SuperException(HttpStatusCode httpStatusCode, string message, Exception exception, object additionalData) : base(message, exception)
        {
            HttpStatusCode = httpStatusCode;
            SystemMessage = GetMessage(httpStatusCode, message, exception, additionalData);
        }

        #region Helper

        private string GetMessage(
            HttpStatusCode httpStatusCode, 
            string message = null, 
            Exception exception = null, 
            object additionalData = null
        )
        {
            string result = "";

            if(!message.HasValue() && exception == null)
            {
                result = $"Message: \n {httpStatusCode.ToString()}";
                result += GetAdditionalData();

                return result;
            }

            if(message.HasValue())
            {
                result = $"Message: \n {message} \n";

                if (exception != null)
                {
                    result += exception.GetNormalizedException(true);
                }

                result += GetAdditionalData();
            }
            else
            {
                if (exception != null)
                {
                    result += exception.GetNormalizedException();
                }

                result += GetAdditionalData();
            }

            string GetAdditionalData()
            {
                if (additionalData == null)
                {
                    return string.Empty;
                }

                return $" \n AdditionalData: \n {additionalData.Serialize()}";
            }

            return result;
        }

        #endregion
    }
}