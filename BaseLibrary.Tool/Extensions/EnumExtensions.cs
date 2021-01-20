using BaseLibrary.Tool.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace BaseLibrary.Tool.Extensions
{
    public static class EnumExtensions
    {
        public static IEnumerable<T> GetEnumValues<T>(
            this T input
        ) where T : struct
        {
            if (!typeof(T).IsEnum)
                throw new NotSupportedException();

            return Enum.GetValues(input.GetType()).Cast<T>();
        }

        public static IEnumerable<T> GetEnumFlags<T>(
            this T input
        ) where T : struct
        {
            if (!typeof(T).IsEnum)
                throw new NotSupportedException();

            foreach (var value in Enum.GetValues(input.GetType()))
                if ((input as Enum).HasFlag(value as Enum))
                    yield return (T)value;
        }

        /// <summary>
        /// Get display from attribute property
        /// </summary>
        /// <param name="value"></param>
        /// <param name="property"></param>
        /// <returns></returns>
        public static string ToDisplay(
            this Enum value, 
            DisplayAttributeProperty property = DisplayAttributeProperty.Name
        )
        {
            var attribute = value.GetType().GetField(value.ToString())
                .GetCustomAttributes<DisplayAttribute>(false).FirstOrDefault();

            if (attribute == null)
                return value.ToString();

            var propValue = attribute.GetType().GetProperty(property.ToString()).GetValue(attribute, null);
            return propValue.ToString();
        }

        /// <summary>
        /// Extract dictionary from enum
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Dictionary<int, string> ToDictionary(
            this Enum value
        )
        {
            return Enum.GetValues(value.GetType()).Cast<Enum>().ToDictionary(p => Convert.ToInt32(p), q => ToDisplay(q));
        }

        /// <summary>
        /// Get enum infos
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static IEnumerable<EnumInfo> GetEnumInfos(
            this Enum value
        )
        {
            return Enum.GetValues(value.GetType()).Cast<Enum>().Select(p => new EnumInfo
            {
                Key = Convert.ToInt32(p),
                Value = p.ToString(),
                Caption = ToDisplay(p)
            });
        }

        /// <summary>
        /// Get enum from int value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T ToEnum<T>(
            this int value
        )
        {
            return (T)Enum.Parse(typeof(T), value.ToString());
        }

        /// <summary>
        /// Get enum from byte value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T ToEnum<T>(
            this byte value
        )
        {
            return (T)Enum.Parse(typeof(T), value.ToString());
        }

        /// <summary>
        /// Get enum from string value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T ToEnum<T>(
            this string value
        )
        {
            return (T)Enum.Parse(typeof(T), value);
        }

        /// <summary>
        /// Get byte value from enum
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static byte ToByte(
            this Enum value
        )
        {
            return Convert.ToByte(value);
        }
    }
}
