using MongoDB.Bson;
using MongoDB.Driver.GridFS;
using System.Threading.Tasks;

namespace BaseLibrary.MongoDB.Interfaces
{
    public interface IGridFsRepository
    {
        GridFSFileInfo GetGridFsInfoById(ObjectId id);
        Task<GridFSFileInfo> GetGridFsInfoByIdAsync(ObjectId id);
        GridFSFileInfo GetGridFsInfoByFileName(string fileName);
        Task<GridFSFileInfo> GetGridFsInfoByFileNameAsync(string fileName);
    }
}