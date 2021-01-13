using Microsoft.AspNetCore.Http;
using MongoDB.Bson;
using MongoDB.Driver.GridFS;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace BaseLibrary.Implementations.Mongo.Extensions
{
    public static class GridFsExtensions
    {
        public static ObjectId UploadFromFile(
            this IGridFSBucket bucket,
            IFormFile source,
            GridFSUploadOptions options = null
        )
        {
            using (var destination = new MemoryStream())
            {
                source.CopyTo(destination);
                var fileName = source.FileName;
                var data = destination.ToArray();

                return bucket.UploadFromBytes(fileName, data, options);
            }
        }

        public static async Task<ObjectId> UploadFromFileAsync(
            this IGridFSBucket bucket,
            IFormFile source,
            GridFSUploadOptions options = null,
            CancellationToken cancellationToken = default
        )
        {
            using (var destination = new MemoryStream())
            {
                source.CopyTo(destination);
                var fileName = source.FileName;
                var data = destination.ToArray();

                return await bucket.UploadFromBytesAsync(fileName, data, options, cancellationToken);
            }
        }
    }
}