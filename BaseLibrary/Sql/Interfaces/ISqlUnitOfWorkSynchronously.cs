using System;
using System.Collections.Generic;
using System.Text;

namespace BaseLibrary.Sql.Interfaces
{
    public interface ISqlUnitOfWorkSynchronously
    {
        ISqlRepository<TEntity> GetRepository<TEntity>()
            where TEntity : class;

        void SaveChanges();
        bool SaveChangesTransaction();
    }
}
