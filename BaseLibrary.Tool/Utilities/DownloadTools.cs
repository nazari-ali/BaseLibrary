using BaseLibrary.Tool.Extensions;
using System;
using System.Net;

namespace BaseLibrary.Tool.Utilities
{
    public static class DownloadTools
    {
        /// <summary>
        /// Download file
        /// </summary>
        /// <param name="url"></param>
        /// <param name="destinationDirectory"></param>
        /// <returns></returns>
        public static string Download(
            string url, 
            string destinationDirectory
        )
        {
            if (!url.HasValue() || !destinationDirectory.HasValue())
            {
                throw new ArgumentNullException("Url and destinition directory can't be null.");
            }

            using WebClient wc = new WebClient();
            wc.DownloadProgressChanged += (_s, _e) =>
            {
                Console.Write("\r" + _e.ProgressPercentage + "%");
            };

            wc.DownloadFileCompleted += (_s, _e) =>
            {

            };

            string destinationUrl = $"{destinationDirectory}{Guid.NewGuid()}{FileTools.GetExtensionByUrl(url)}";
            wc.DownloadFileAsync(new Uri(url), destinationUrl);

            return destinationUrl;
        }
    }
}