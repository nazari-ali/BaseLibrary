using BaseLibrary.Tool.Extensions;
using MimeTypes;
using System.IO;
using System.Net;

namespace BaseLibrary.Tool.Utilities
{
    public static class FileTools
    {
        /// <summary>
        /// Create folder
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool CreateDirectory(
            string path
        )
        {
            if (!path.HasValue()) 
                return false;

            Directory.CreateDirectory(path);
            return true;
        }

        /// <summary>
        /// Remove directory
        /// </summary>
        /// <param name="path"></param>
        /// <param name="recursive"></param>
        /// <returns></returns>
        public static bool RemoveDirectory(
            string path,
            bool recursive = false
        )
        {
            if (!path.HasValue() || !Directory.Exists(path))
            {
                return false;
            }

            Directory.Delete(path, recursive);
            return true;
        }

        /// <summary>
        /// Copy file from origin to destination
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="overwrite"></param>
        /// <returns></returns>
        public static bool CopyFilePathToPath(
            string from,
            string to,
            bool overwrite = false
        )
        {
            if (!from.HasValue() || !File.Exists(from) || !to.HasValue() || (!overwrite && File.Exists(to)))
            {
                return false;
            }

            CreateDirectory(
                Path.GetDirectoryName(to)
            );

            File.Copy(from, to, overwrite);
            return true;
        }

        /// <summary>
        /// Move file from origin to destination
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="overwrite"></param>
        /// <returns></returns>
        public static bool MoveFilePathToPath(
            string from,
            string to,
            bool overwrite = false
        )
        {
            if (!from.HasValue() || !File.Exists(from) || !to.HasValue() || (!overwrite && File.Exists(to)))
            {
                return false;
            }

            CreateDirectory(
                Path.GetDirectoryName(to)
            );

            File.Move(from, to, overwrite);
            return true;
        }

        /// <summary>
        /// Remove file
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool RemoveFile(
            string path
        )
        {
            if (!path.HasValue() || !File.Exists(path))
            {
                return false;
            }

            File.Delete(path);
            return true;
        }

        /// <summary>
        /// Get file Extension by content type
        /// </summary>
        /// <param name="contentType"></param>
        /// <returns></returns>
        public static string GetExtensionByContentType(
            string contentType
        )
        {
            return MimeTypeMap.GetExtension(contentType).ToLower().Trim();
        }

        /// <summary>
        /// Get file Extension by url
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string GetExtensionByUrl(
            string url
        )
        {
            return GetExtensionByContentType(GetContentType(url));
        }

        /// <summary>
        /// Get content type
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string GetContentType(
            string url
        )
        {
            var contentType = "";
            var request = WebRequest.Create(url) as HttpWebRequest;

            if (request.GetResponse() is HttpWebResponse response)
                contentType = response.ContentType;

            return contentType.ToLower().Trim();
        }

        /// <summary>
        /// Copy directory and subCategories
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="copySubDirectories"></param>
        public static void DirectoryCopy(
            string from,
            string to,
            bool copySubDirectories = true
        )
        {
            DirectoryInfo dir = new DirectoryInfo(from);
            DirectoryInfo[] dirs = dir.GetDirectories();

            // If the source directory does not exist, throw an exception.
            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException($"Source directory does not exist or could not be found: {from}");
            }

            // If the destination directory does not exist, create it.
            if (!Directory.Exists(to))
            {
                CreateDirectory(to);
            }

            // Get the file contents of the directory to copy.
            FileInfo[] files = dir.GetFiles();

            foreach (FileInfo file in files)
            {
                // Create the path to the new copy of the file.
                string tempPath = Path.Combine(to, file.Name);

                // Copy the file.
                file.CopyTo(tempPath, false);
            }

            // If copySubDirs is true, copy the subdirectories.
            if (copySubDirectories)
            {
                foreach (DirectoryInfo directoryInfo in dirs)
                {
                    // Create the subDirectory.
                    string tempPath = Path.Combine(to, directoryInfo.Name);

                    // Copy the subDirectories.
                    DirectoryCopy(directoryInfo.FullName, tempPath, true);
                }
            }
        }
    }
}