using BaseLibrary.Sql.Models.Interfaces;
using System.Linq;

namespace BaseLibrary.Sql.Repository.Interfaces
{
    public interface ISqlRepository<TEntity> : ISqlRepositorySynchronously<TEntity>, ISqlRepositoryAsynchronously<TEntity>, ISqlEntity where TEntity : class
    {
        
    }
}