using System;

namespace BaseLibrary.Extensions
{
    public static class ExceptionExtensions
    {
        /// <summary>
        /// Get inner exception message
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        public static string GetInnerExceptionMessage(
            this Exception exception
        )
        {
            Exception eDetail = exception;

            while (eDetail.InnerException != null)
                eDetail = eDetail.InnerException;

            return eDetail.Message;
        }
    }
}