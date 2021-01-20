using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

namespace BaseLibrary.Tool.Extensions
{
    public static class StreamExtensions
    {
        /// <summary>
        /// Get byte from IFromFile
        /// </summary>
        /// <param name="formFile"></param>
        /// <returns></returns>
        public static byte[] GetBytes(
            this IFormFile formFile
        )
        {
            using (var memoryStream = new MemoryStream())
            {
                formFile.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }

        /// <summary>
        /// Get byte from IFromFile
        /// </summary>
        /// <param name="formFile"></param>
        /// <returns></returns>
        public static async Task<byte[]> GetBytesAsync(
            this IFormFile formFile
        )
        {
            using (var memoryStream = new MemoryStream())
            {
                await formFile.CopyToAsync(memoryStream);
                return memoryStream.ToArray();
            }
        }
    }
}
