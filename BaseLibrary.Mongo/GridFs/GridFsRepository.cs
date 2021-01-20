using BaseLibrary.Mongo.DbContext.Interfaces;
using BaseLibrary.Mongo.GridFs.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using System.Threading.Tasks;

namespace BaseLibrary.Mongo.GridFs
{
    public class GridFsRepository : IGridFsRepository
    {
        protected readonly IGridFSBucket Bucket;

        public GridFsRepository(IMongoContext mongoContext)
        {
            Bucket = mongoContext.Bucket;
        }

        public GridFSFileInfo Get(ObjectId objectId)
        {
            var filter = Builders<GridFSFileInfo>.Filter.Eq("_id", objectId);
            return Bucket.Find(filter).FirstOrDefault();
        }

        public async Task<GridFSFileInfo> GetAsync(ObjectId objectId)
        {
            var filter = Builders<GridFSFileInfo>.Filter.Eq("_id", objectId);
            return await Bucket.FindAsync(filter).Result.FirstOrDefaultAsync();
        }

        public GridFSFileInfo Get(string fileName)
        {
            var filter = Builders<GridFSFileInfo>.Filter.Eq("filename", fileName);
            return Bucket.Find(filter).FirstOrDefault();
        }

        public async Task<GridFSFileInfo> GetAsync(string fileName)
        {
            var filter = Builders<GridFSFileInfo>.Filter.Eq("filename", fileName);
            return await Bucket.FindAsync(filter).Result.FirstOrDefaultAsync();
        }

        public bool Exist(ObjectId objectId)
        {
            var filter = Builders<GridFSFileInfo>.Filter.Eq("_id", objectId);
            return Bucket.Find(filter).Any();
        }

        public Task<bool> ExistAsync(ObjectId objectId)
        {
            var filter = Builders<GridFSFileInfo>.Filter.Eq("_id", objectId);
            return Bucket.FindAsync(filter).Result.AnyAsync();
        }

        public bool Exist(string fileName)
        {
            var filter = Builders<GridFSFileInfo>.Filter.Eq("filename", fileName);
            return Bucket.Find(filter).Any();
        }

        public async Task<bool> ExistAsync(string fileName)
        {
            var filter = Builders<GridFSFileInfo>.Filter.Eq("filename", fileName);
            return await Bucket.FindAsync(filter).Result.AnyAsync();
        }

        public BsonDocument GetMetadata(ObjectId objectId)
        {
            var info = Get(objectId);
            return info?.Metadata;
        }

        public async Task<BsonDocument> GetMetadataAsync(ObjectId objectId)
        {
            var info = await GetAsync(objectId);
            return info?.Metadata;
        }

        public string GetMetadata(ObjectId objectId, string element)
        {
            var info = Get(objectId);
            return info?.Metadata[element].ToString();
        }

        public async Task<string> GetMetadataAsync(ObjectId objectId, string element)
        {
            var info = await GetAsync(objectId);
            return info?.Metadata[element].ToString();
        }
    }
}