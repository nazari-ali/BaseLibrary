using System;

namespace BaseLibrary.Utilities
{
    public static class UtilityTools
    {
        /// <summary>
        /// Get random code
        /// </summary>
        /// <param name="numberOfCode"></param>
        /// <returns></returns>
        public static int GetRandomCode(
            int numberOfCode = 4
        )
        {
            var minValue = Convert.ToInt32("1".PadRight(numberOfCode, '0'));
            var maxValue = Convert.ToInt32("9".PadRight(numberOfCode, '9'));

            Random random = new Random();
            return random.Next(minValue, maxValue);
        }
    }
}