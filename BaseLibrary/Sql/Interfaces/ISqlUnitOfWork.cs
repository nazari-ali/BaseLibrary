using System;
using System.Threading.Tasks;

namespace BaseLibrary.Sql.Interfaces
{
    public interface ISqlUnitOfWork : ISqlUnitOfWorkSynchronously, ISqlUnitOfWorkAsynchronously, IDisposable
    {
        
    }
}