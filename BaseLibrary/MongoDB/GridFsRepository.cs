using BaseLibrary.Models.MongoDB;
using BaseLibrary.MongoDB.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using System.Threading.Tasks;

namespace BaseLibrary.MongoDB
{
    public class GridFsRepository : IGridFsRepository
    {
        protected readonly IGridFSBucket Bucket;

        public GridFsRepository(IMongoContext mongoContext)
        {
            Bucket = mongoContext.Bucket;
        }

        public GridFSFileInfo GetGridFsInfoById(ObjectId objectId)
        {
            var filter = Builders<GridFSFileInfo>.Filter.Eq("_id", objectId);
            return Bucket.Find(filter).FirstOrDefault();
        }

        public async Task<GridFSFileInfo> GetGridFsInfoByIdAsync(ObjectId objectId)
        {
            var filter = Builders<GridFSFileInfo>.Filter.Eq("_id", objectId);
            return await Bucket.FindAsync(filter).Result.FirstOrDefaultAsync();
        }

        public GridFSFileInfo GetGridFsInfoByFileName(string fileName)
        {
            var filter = Builders<GridFSFileInfo>.Filter.Eq("filename", fileName);
            return Bucket.Find(filter).FirstOrDefault();
        }

        public async Task<GridFSFileInfo> GetGridFsInfoByFileNameAsync(string fileName)
        {
            var filter = Builders<GridFSFileInfo>.Filter.Eq("filename", fileName);
            return await Bucket.FindAsync(filter).Result.FirstOrDefaultAsync();
        }

        public BsonDocument GetMetadata(ObjectId objectId)
        {
            var info = GetGridFsInfoById(objectId);
            return info?.Metadata;
        }

        public async Task<BsonDocument> GetMetadataAsync(ObjectId objectId)
        {
            var info = await GetGridFsInfoByIdAsync(objectId);
            return info?.Metadata;
        }

        public string GetMetadata(ObjectId objectId, string element)
        {
            var info = GetGridFsInfoById(objectId);
            return info?.Metadata[element].ToString();
        }

        public async Task<string> GetMetadataAsync(ObjectId objectId, string element)
        {
            var info = await GetGridFsInfoByIdAsync(objectId);
            return info?.Metadata[element].ToString();
        }
    }
}