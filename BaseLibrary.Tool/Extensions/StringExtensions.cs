using MongoDB.Bson;
using System;

namespace BaseLibrary.Tool.Extensions
{
    public static class StringExtensions
    {
        #region Properties

        private static readonly string[] _fa = { "۰", "۱", "۲", "۳", "۴", "۵", "۶", "۷", "۸", "۹" };
        private static readonly string[] _en = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };

        #endregion

        /// <summary>
        /// Check empty and null value 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="ignoreWhiteSpace"></param>
        /// <returns></returns>
        public static bool HasValue(
            this string value, 
            bool ignoreWhiteSpace = true
        )
        {
            return ignoreWhiteSpace ? !string.IsNullOrWhiteSpace(value) : !string.IsNullOrEmpty(value);
        }

        /// <summary>
        /// Cast string to guid
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Guid ToGuid(
            this string value
        )
        {
            try
            {
                return Guid.Parse(value);
            }
            catch
            {
                return default;
            }
        }

        /// <summary>
        /// Cast string to objectId
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static ObjectId ToObjectId(
            this string value
        )
        {
            try
            {
                return ObjectId.Parse(value);
            }
            catch
            {
                return default;
            }
        }

        /// <summary>
        /// Replace english number to persian number
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToPersianNumber(
            this string value
        )
        {
            string chash = value;
            for (int i = 0; i < 10; i++)
                chash = chash.Replace(_en[i], _fa[i]);
            return chash;
        }

        /// <summary>
        /// Replace persian number to english number
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToEnglishNumber(
            this string value
        )
        {
            string chash = value;
            for (int i = 0; i < 10; i++)
                chash = chash.Replace(_fa[i], _en[i]);
            return chash;
        }

        /// <summary>
        /// Fix problem arabic character
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string FixPersianChars(
            this string value
        )
        {
            return value.Replace("ﮎ", "ک")
                .Replace("ﮏ", "ک")
                .Replace("ﮐ", "ک")
                .Replace("ﮑ", "ک")
                .Replace("ك", "ک")
                .Replace("ي", "ی")
                .Replace(" ", " ")
                .Replace("‌", " ")
                .Replace("ھ", "ه");//.Replace("ئ", "ی");
        }

        /// <summary>
        /// Clean string value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string CleanString(
            this string value
        )
        {
            return value.Trim().FixPersianChars().ToPersianNumber().NullIfEmpty();
        }

        /// <summary>
        /// return null if value null or empty or length = 0
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string NullIfEmpty(
            this string value
        )
        {
            return value?.Length == 0 ? null : value;
        }

        /// <summary>
        /// Checks string object's value to array of string values
        /// </summary>        
        /// <param name="stringValues">Array of string values to compare</param>
        /// <returns>Return true if any string value matches</returns>
        public static bool In(
            this string value, 
            params string[] stringValues
        )
        {
            foreach (string otherValue in stringValues)
                if (string.Compare(value, otherValue) == 0)
                    return true;

            return false;
        }

        /// <summary>
        ///  Replaces the format item in a specified System.String with the text equivalent
        ///  of the value of a specified System.Object instance.
        /// </summary>
        /// <param name="value">A composite format string</param>
        /// <param name="arg0">An System.Object to format</param>
        /// <returns>A copy of format in which the first format item has been replaced by the
        /// System.String equivalent of arg0</returns>
        public static string Format(
            this string value, 
            object arg0
        )
        {
            return string.Format(value, arg0);
        }

        /// <summary>
        ///  Replaces the format item in a specified System.String with the text equivalent
        ///  of the value of a specified System.Object instance.
        /// </summary>
        /// <param name="value">A composite format string</param>
        /// <param name="args">An System.Object array containing zero or more objects to format.</param>
        /// <returns>A copy of format in which the format items have been replaced by the System.String
        /// equivalent of the corresponding instances of System.Object in args.</returns>
        public static string Format(
            this string value, 
            params object[] args
        )
        {
            return string.Format(value, args);
        }

        /// <summary>
        /// Checks if a string value is numeric according to you system culture.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsNumeric(
            this string value
        )
        {
            long retNum;
            return long.TryParse(value, System.Globalization.NumberStyles.Integer, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);
        }
    }
}