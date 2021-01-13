using MongoDB.Driver;
using System;
using System.Threading.Tasks;

namespace BaseLibrary.MongoDB.Interfaces
{
    public interface IMongoUnitOfWork : IDisposable
    {
        IClientSessionHandle Session { get; }
        Task<IClientSessionHandle> SessionAsync { get; }
    }
}
