using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace BaseLibrary.Tool.Extensions
{
    public static class JsonExtensions
    {
        /// <summary>
        /// A JSON string representation of the object.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="isCamelCase"></param>
        /// <returns></returns>
        public static string Serialize(
            this object obj, 
            bool isCamelCase = false
        )
        {
            if (!isCamelCase)
                return JsonConvert.SerializeObject(obj);

            var serializerSettings = new JsonSerializerSettings();
            serializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            return JsonConvert.SerializeObject(obj, serializerSettings);
        }

        /// <summary>
        /// A JSON string representation of the object.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="recursionDepth"></param>
        /// <returns></returns>
        public static string Serialize(
            this object obj, 
            int recursionDepth
        )
        {
            return JsonConvert.SerializeObject(obj, new JsonSerializerSettings
            {
                MaxDepth = recursionDepth
            });
        }

        /// <summary>
        /// A JSON string representation of the object.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static string Serialize(
            this object obj,
            JsonSerializerSettings options
        )
        {
            return JsonConvert.SerializeObject(obj, options);
        }

        /// <summary>
        /// The deserialized object from the JSON string.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="serialized"></param>
        /// <returns></returns>
        public static T Deserialize<T>(
            this string serialized
        )
        {
            return JsonConvert.DeserializeObject<T>(serialized);
        }

        /// <summary>
        /// The deserialized object from the JSON string.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="serialized"></param>
        /// <returns></returns>
        public static object Deserialize(
            this string serialized
        )
        {
            return JsonConvert.DeserializeObject(serialized);
        }
    }
}