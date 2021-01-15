using MongoDB.Bson;
using MongoDB.Driver.GridFS;
using System.Threading.Tasks;

namespace BaseLibrary.MongoDB.Interfaces
{
    public interface IGridFsRepository
    {
        GridFSFileInfo GetGridFsInfoById(ObjectId objectId);
        Task<GridFSFileInfo> GetGridFsInfoByIdAsync(ObjectId objectId);
        GridFSFileInfo GetGridFsInfoByFileName(string fileName);
        Task<GridFSFileInfo> GetGridFsInfoByFileNameAsync(string fileName);
        BsonDocument GetMetadata(ObjectId objectId);
        Task<BsonDocument> GetMetadataAsync(ObjectId objectId);
        string GetMetadata(ObjectId objectId, string element);
        Task<string> GetMetadataAsync(ObjectId objectId, string element);
    }
}