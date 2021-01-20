using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace BaseLibrary.Tool.Extensions
{
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Gets the 12:00:00 instance of a DateTime
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime AbsoluteStart(
            this DateTime date
        )
        {
            return date.Date;
        }

        /// <summary>
        /// Gets the 11:59:59 instance of a DateTime
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime AbsoluteEnd(
            this DateTime date
        )
        {
            return AbsoluteStart(date).AddDays(1).AddTicks(-1);
        }

        /// <summary>
        /// A simple date range
        /// </summary>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        public static IEnumerable<DateTime> GetDateRangeTo(
            this DateTime fromDate, 
            DateTime toDate
        )
        {
            var range = Enumerable.Range(0, new TimeSpan(toDate.Ticks - fromDate.Ticks).Days);

            return from p in range
                   select fromDate.Date.AddDays(p);
        }

        /// <summary>
        /// Get the actual age of a person
        /// </summary>
        /// <param name="dateOfBirth"></param>
        /// <returns></returns>
        public static int Age(
            this DateTime dateOfBirth
        )
        {
            if (DateTime.Today.Month < dateOfBirth.Month || DateTime.Today.Month == dateOfBirth.Month && DateTime.Today.Day < dateOfBirth.Day)
            {
                return DateTime.Today.Year - dateOfBirth.Year - 1;
            }

            return DateTime.Today.Year - dateOfBirth.Year;
        }

        /// <summary>
        /// Checks if the date is between the two provided dates
        /// </summary>
        /// <param name="date"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="compareTime"></param>
        /// <returns></returns>
        public static bool IsBetween(
            this DateTime date,
            DateTime startDate,
            DateTime endDate, 
            bool compareTime = false
        )
        {
            return compareTime 
                ? date >= startDate && date <= endDate 
                : date.Date >= startDate.Date && date.Date <= endDate.Date;
        }

        /// <summary>
        /// Gets the last date of the month of the DateTime.
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime GetLastDayOfMonth(
            this DateTime date
        )
        {
            return new DateTime(date.Year, date.Month, 1).AddMonths(1).AddDays(-1);
        }

        /// <summary>
        /// Returns datetime corresponding to day end
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime GetEndOfTheDay(
            this DateTime date
        )
        {
            return new DateTime(date.Year, date.Month, date.Day, 23, 59, 59, 999);
        }

        /// <summary>
        /// Converts a System.DateTime object to Unix timestamp
        /// </summary>
        /// <returns>The Unix timestamp</returns>
        public static long ToUnixTimestamp(
            this DateTime date
        )
        {
            DateTime unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0);
            TimeSpan unixTimeSpan = date - unixEpoch;

            return (long)unixTimeSpan.TotalSeconds;
        }

        /// <summary>
        /// DateDiff in SQL style. 
        /// Datepart implemented: 
        ///     "year" (abbr. "yy", "yyyy"), 
        ///     "quarter" (abbr. "qq", "q"), 
        ///     "month" (abbr. "mm", "m"), 
        ///     "day" (abbr. "dd", "d"), 
        ///     "week" (abbr. "wk", "ww"), 
        ///     "hour" (abbr. "hh"), 
        ///     "minute" (abbr. "mi", "n"), 
        ///     "second" (abbr. "ss", "s"), 
        ///     "millisecond" (abbr. "ms").
        /// </summary>
        /// <param name="DatePart"></param>
        /// <param name="EndDate"></param>
        /// <returns></returns>
        public static long DateDiff(
            this DateTime StartDate, 
            string DatePart, 
            DateTime EndDate
        )
        {
            long DateDiffVal = 0;
            System.Globalization.Calendar cal = Thread.CurrentThread.CurrentCulture.Calendar;
            TimeSpan ts = new TimeSpan(EndDate.Ticks - StartDate.Ticks);

            switch (DatePart.ToLower().Trim())
            {
                #region year
                case "year":
                case "yy":
                case "yyyy":
                    DateDiffVal = (long)(cal.GetYear(EndDate) - cal.GetYear(StartDate));
                    break;
                #endregion

                #region quarter
                case "quarter":
                case "qq":
                case "q":
                    DateDiffVal = (long)((((cal.GetYear(EndDate) - cal.GetYear(StartDate)) * 4) + ((cal.GetMonth(EndDate) - 1) / 3)) - ((cal.GetMonth(StartDate) - 1) / 3));
                    break;
                #endregion

                #region month
                case "month":
                case "mm":
                case "m":
                    DateDiffVal = (long)(((cal.GetYear(EndDate) - cal.GetYear(StartDate)) * 12 + cal.GetMonth(EndDate)) - cal.GetMonth(StartDate));
                    break;
                #endregion

                #region day
                case "day":
                case "d":
                case "dd":
                    DateDiffVal = (long)ts.TotalDays;
                    break;
                #endregion

                #region week
                case "week":
                case "wk":
                case "ww":
                    DateDiffVal = (long)(ts.TotalDays / 7);
                    break;
                #endregion

                #region hour
                case "hour":
                case "hh":
                    DateDiffVal = (long)ts.TotalHours;
                    break;
                #endregion

                #region minute
                case "minute":
                case "mi":
                case "n":
                    DateDiffVal = (long)ts.TotalMinutes;
                    break;
                #endregion

                #region second
                case "second":
                case "ss":
                case "s":
                    DateDiffVal = (long)ts.TotalSeconds;
                    break;
                #endregion

                #region millisecond
                case "millisecond":
                case "ms":
                    DateDiffVal = (long)ts.TotalMilliseconds;
                    break;
                #endregion

                default:
                    throw new Exception(String.Format("DatePart \"{0}\" is unknown", DatePart));
            }

            return DateDiffVal;
        }

        /// <summary>
        /// Gets a nullable DateTime object from a string input. Good for grabbing datetimes from user inputs, like textboxes and querystrings.
        /// </summary>
        /// <param name="dateString"></param>
        /// <returns></returns>
        public static DateTime? ToDateTime(
            this string dateString
        )
        {
            DateTime dtr;
            var tryDtr = DateTime.TryParse(dateString, out dtr);
            return (tryDtr) ? dtr : new DateTime?();
        }

        /// <summary>
        /// Returns whether or not a DateTime is during a leap year.
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static bool IsLeapYear(
            this DateTime date
        )
        {
            return (System.DateTime.DaysInMonth(date.Year, 2) == 29);
        }

        /// <summary>
        /// The idea behind the ToFriendlyDateString() method is representing dates in a user friendly way. For example, when displaying a news article on a webpage,
        /// ou might want articles that were published one day ago to have their publish dates represented as "yesterday at 12:30 PM". Or if the article was publish today,
        /// show the date as "Today, 3:33 PM".
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string ToFriendlyDateString(
            this DateTime date
        )
        {
            string FormattedDate = "";
            if (date.Date == DateTime.Today)
            {
                FormattedDate = "Today";
            }
            else if (date.Date == DateTime.Today.AddDays(-1))
            {
                FormattedDate = "Yesterday";
            }
            else if (date.Date > DateTime.Today.AddDays(-6))
            {
                // *** Show the Day of the week
                FormattedDate = date.ToString("dddd").ToString();
            }
            else
            {
                FormattedDate = date.ToString("MMMM dd, yyyy");
            }

            //append the time portion to the output
            FormattedDate += " @ " + date.ToString("t").ToLower();
            return FormattedDate;
        }

        /// <summary>
        /// Get the elapsed time since the input DateTime
        /// </summary>
        /// <param name="date">Input DateTime</param>
        /// <returns>Returns a TimeSpan value with the elapsed time since the input DateTime</returns>
        /// <example>
        /// TimeSpan elapsed = dtStart.Elapsed();
        /// </example>
        /// <seealso cref="ElapsedSeconds()"/>
        public static TimeSpan Elapsed(
            this DateTime date
        )
        {
            return DateTime.Now.Subtract(date);
        }

        /// <summary>
        /// Convert DateTime to Shamsi Date (YYYY/MM/DD)
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string ToShamsiDateYMD(
            this DateTime date
        )
        {
            System.Globalization.PersianCalendar PC = new System.Globalization.PersianCalendar();
            int intYear = PC.GetYear(date);
            int intMonth = PC.GetMonth(date);
            int intDay = PC.GetDayOfMonth(date);

            return (intYear.ToString() + "/" + intMonth.ToString() + "/" + intDay.ToString());
        }

        /// <summary>
        /// Convert DateTime to Shamsi Date (DD/MM/YYYY)
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string ToShamsiDateDMY(
            this DateTime date
        )
        {
            System.Globalization.PersianCalendar PC = new System.Globalization.PersianCalendar();
            int intYear = PC.GetYear(date);
            int intMonth = PC.GetMonth(date);
            int intDay = PC.GetDayOfMonth(date);

            return (intDay.ToString() + "/" + intMonth.ToString() + "/" + intYear.ToString());
        }

        /// <summary>
        /// Convert DateTime to Shamsi String
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string ToShamsiDateString(
            this DateTime date
        )
        {
            System.Globalization.PersianCalendar PC = new System.Globalization.PersianCalendar();
            int intYear = PC.GetYear(date);
            int intMonth = PC.GetMonth(date);
            int intDayOfMonth = PC.GetDayOfMonth(date);
            DayOfWeek enDayOfWeek = PC.GetDayOfWeek(date);
            string strMonthName, strDayName;

            switch (intMonth)
            {
                case 1:
                    strMonthName = "فروردین";
                    break;
                case 2:
                    strMonthName = "اردیبهشت";
                    break;
                case 3:
                    strMonthName = "خرداد";
                    break;
                case 4:
                    strMonthName = "تیر";
                    break;
                case 5:
                    strMonthName = "مرداد";
                    break;
                case 6:
                    strMonthName = "شهریور";
                    break;
                case 7:
                    strMonthName = "مهر";
                    break;
                case 8:
                    strMonthName = "آبان";
                    break;
                case 9:
                    strMonthName = "آذر";
                    break;
                case 10:
                    strMonthName = "دی";
                    break;
                case 11:
                    strMonthName = "بهمن";
                    break;
                case 12:
                    strMonthName = "اسفند";
                    break;
                default:
                    strMonthName = "";
                    break;
            }

            switch (enDayOfWeek)
            {
                case DayOfWeek.Friday:
                    strDayName = "جمعه";
                    break;
                case DayOfWeek.Monday:
                    strDayName = "دوشنبه";
                    break;
                case DayOfWeek.Saturday:
                    strDayName = "شنبه";
                    break;
                case DayOfWeek.Sunday:
                    strDayName = "یکشنبه";
                    break;
                case DayOfWeek.Thursday:
                    strDayName = "پنجشنبه";
                    break;
                case DayOfWeek.Tuesday:
                    strDayName = "سه شنبه";
                    break;
                case DayOfWeek.Wednesday:
                    strDayName = "چهارشنبه";
                    break;
                default:
                    strDayName = "";
                    break;
            }

            return (string.Format("{0} {1} {2} {3}", strDayName, intDayOfMonth, strMonthName, intYear));
        }

        /// <summary>
        /// Returns a formatted date or emtpy string
        /// </summary>
        /// <param name="date">DateTime instance or null</param>
        /// <param name="format">datetime formatstring </param>
        /// <returns></returns>
        public static string ToString(
            this DateTime? date,
            string format
        )
        {
            if (date != null)
            {
                return date.Value.ToString(format);
            }

            return "";
        }

        /// <summary>
        /// Converts a regular DateTime to a RFC822 date string.
        /// </summary>
        /// <returns>The specified date formatted as a RFC822 date string.</returns>
        public static string ToRFC822DateString(
            this DateTime date
        )
        {
            int offset = TimeZone.CurrentTimeZone.GetUtcOffset(DateTime.Now).Hours;
            string timeZone = "+" + offset.ToString().PadLeft(2, '0');

            if (offset < 0)
            {
                int i = offset * -1;
                timeZone = "-" + i.ToString().PadLeft(2, '0');
            }

            return date.ToString("ddd, dd MMM yyyy HH:mm:ss " + timeZone.PadRight(5, '0'), System.Globalization.CultureInfo.GetCultureInfo("en-US"));
        }

        /// <summary>
        /// Adds time to existing DateTime
        /// </summary>
        /// <param name="date"></param>
        /// <param name="hour"></param>
        /// <param name="minutes"></param>
        /// <returns></returns>
        public static DateTime AddTime(
            this DateTime date, 
            int hour, 
            int minutes
        )
        {
            return date + new TimeSpan(hour, minutes, 0);
        }

        /// <summary>
        /// Inspiration for this extension method was another DateTime extension that determines difference in current time and a DateTime object.
        /// That one returned a string and it is more useful for my applications to have a TimeSpan reference instead. That is what I did with this extension method.
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static TimeSpan TimeElapsed(
            this DateTime date
        )
        {
            return DateTime.Now - date;
        }
    }
}