using MongoDB.Driver;
using System;
using System.Threading.Tasks;

namespace BaseLibrary.Mongo.UnitOfWork.Interfaces
{
    public interface IMongoUnitOfWork : IDisposable
    {
        void SaveChanges();
        bool SaveChangesTransaction();
        Task<bool> SaveChangesTransactionAsync();
    }
}
