using System;
using System.Threading.Tasks;

namespace BaseLibrary.Sql.UnitOfWork.Interfaces
{
    public interface ISqlUnitOfWork : ISqlUnitOfWorkSynchronously, ISqlUnitOfWorkAsynchronously, IDisposable
    {
        
    }
}