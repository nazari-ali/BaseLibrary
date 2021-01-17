using System;
using System.Collections.Generic;
using System.Text;

namespace BaseLibrary.Exceptions.Helper
{
    internal static class LocalExceptionMessage
    {
        internal static string NotFoundMessage => "The requested information was not found.";
        internal static string ForbiddenMessage => "You do not have access to this section.";
        internal static string InternalServerErrorMessage => "There is a problem with the system.";
        internal static string NotContentMessage => "The operation was successful, but unfortunately no content was found.";
        internal static string BadRequestMessage => "The submitted information is incorrect.";
        internal static string PaymentMessage => "Subscribe to access this section.";
        internal static string UnauthorizedMessage => "The entered identity information is not valid.";
        internal static string UpgradeRequiredMessage => "Install the new version to use the app.";
    }
}
