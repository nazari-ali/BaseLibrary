using MongoDB.Bson;
using MongoDB.Driver.GridFS;
using System.Threading.Tasks;

namespace BaseLibrary.MongoDB.Interfaces
{
    public interface IGridFsRepository
    {
        GridFSFileInfo Get(ObjectId objectId);
        Task<GridFSFileInfo> GetAsync(ObjectId objectId);
        GridFSFileInfo Get(string fileName);
        Task<GridFSFileInfo> GetAsync(string fileName);
        bool Exist(ObjectId objectId);
        Task<bool> ExistAsync(ObjectId objectId);
        bool Exist(string fileName);
        Task<bool> ExistAsync(string fileName);
        BsonDocument GetMetadata(ObjectId objectId);
        Task<BsonDocument> GetMetadataAsync(ObjectId objectId);
        string GetMetadata(ObjectId objectId, string element);
        Task<string> GetMetadataAsync(ObjectId objectId, string element);
    }
}