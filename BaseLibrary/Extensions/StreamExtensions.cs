using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

namespace BaseLibrary.Extensions
{
    public static class StreamExtensions
    {
        /// <summary>
        /// Get byte from IFromFile
        /// </summary>
        /// <param name="formFile"></param>
        /// <returns></returns>
        public static async Task<byte[]> GetBytes(
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
