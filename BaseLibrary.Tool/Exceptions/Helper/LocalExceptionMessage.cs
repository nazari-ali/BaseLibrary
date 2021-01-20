using System;
using System.Collections.Generic;
using System.Text;

namespace BaseLibrary.Tool.Exceptions.Helper
{
    public static class LocalExceptionMessage
    {
        public static string SuccessdMessage => "mission accomplished.";
        public static string NotFoundMessage => "The requested information was not found.";
        public static string ForbiddenMessage => "You do not have access to this section.";
        public static string InternalServerErrorMessage => "There is a problem with the system.";
        public static string NotContentMessage => "The operation was successful, but unfortunately no content was found.";
        public static string BadRequestMessage => "The submitted information is incorrect.";
        public static string PaymentMessage => "Subscribe to access this section.";
        public static string UnauthorizedMessage => "The entered identity information is not valid.";
        public static string UpgradeRequiredMessage => "Install the new version to use the app.";
    }
}
