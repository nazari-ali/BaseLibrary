using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace BaseLibrary.Extensions
{
    public static class ObjectExtensions
    {
        /// <summary>
        /// Cast object to list for type TModel
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static List<TModel> ToList<TModel>(
            this object value
        )
        {
            return ((IList)value).Cast<TModel>().ToList();
        }

        /// <summary>
        /// Get item count
        /// if object type from list, get item count 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static long GetItemCount(
            this object value
        )
        {
            return ((IList)value).Count;
        }

        /// <summary>
        /// Makes a copy from the object.
        /// Doesn't copy the reference memory, only data.
        /// </summary>
        /// <typeparam name="T">Type of the return object.</typeparam>
        /// <param name="item">Object to be copied.</param>
        /// <returns>Returns the copied object.</returns>
        public static T Clone<T>(
            this object item
        )
        {
            if (item != null)
            {
                BinaryFormatter formatter = new BinaryFormatter();

                using (MemoryStream stream = new MemoryStream())
                {
                    formatter.Serialize(stream, item);
                    stream.Seek(0, SeekOrigin.Begin);

                    return (T)formatter.Deserialize(stream);
                }
            }
            
            return default(T);
        }
    }
}
