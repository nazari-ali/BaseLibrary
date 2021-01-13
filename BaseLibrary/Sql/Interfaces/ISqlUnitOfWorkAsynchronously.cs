using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BaseLibrary.Sql.Interfaces
{
    public interface ISqlUnitOfWorkAsynchronously
    {
        Task SaveChangesAsync();
        Task<bool> SaveChangesTransactionAsync();
    }
}