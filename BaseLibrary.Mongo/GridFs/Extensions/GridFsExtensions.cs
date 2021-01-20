using BaseLibrary.Tool.Extensions;
using Microsoft.AspNetCore.Http;
using MongoDB.Bson;
using MongoDB.Driver.GridFS;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace BaseLibrary.Mongo.GridFs.Extensions
{
    public static class GridFsExtensions
    {
        public static ObjectId UploadFromFile(
            this IGridFSBucket bucket,
            IFormFile source,
            GridFSUploadOptions options = null
        )
        {
            var fileName = source.FileName;
            var data = source.GetBytes();

            return bucket.UploadFromBytes(fileName, data, options);
        }

        public static async Task<ObjectId> UploadFromFileAsync(
            this IGridFSBucket bucket,
            IFormFile source,
            GridFSUploadOptions options = null,
            CancellationToken cancellationToken = default
        )
        {
            var fileName = source.FileName;
            var data = await source.GetBytesAsync();

            return await bucket.UploadFromBytesAsync(fileName, data, options, cancellationToken);
        }
    }
}