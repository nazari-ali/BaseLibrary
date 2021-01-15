using BaseLibrary.Sql.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaseLibrary.Sql
{
    public class SqlUnitOfWork : ISqlUnitOfWork
    {
        protected readonly SqlDbContext Context;
        private readonly Dictionary<Type, object> _repositories;

        public SqlUnitOfWork(DbContext dbContext)
        {
            Context = (SqlDbContext)dbContext;
            _repositories = new Dictionary<Type, object>();
        }

        /// <summary>
        /// SaveChanges
        /// </summary>
        /// <returns></returns>
        public void SaveChanges()
        {
            Context.SaveChanges();
        }

        /// <summary>
        /// SaveChanges
        /// </summary>
        /// <returns></returns>
        public Task SaveChangesAsync()
        {
            return Context.SaveChangesAsync();
        }

        /// <summary>
        /// SaveChanges Transaction
        /// for all command
        /// </summary>
        /// <returns></returns>
        public bool SaveChangesTransaction()
        {
            bool returnValue = true;
            using var dbContextTransaction = Context.Database.BeginTransaction();

            try
            {
                Context.SaveChanges();
                dbContextTransaction.Commit();
            }
            catch (Exception)
            {
                //Log Exception Handling message                      
                returnValue = false;
                dbContextTransaction.Rollback();
            }

            return returnValue;
        }

        /// <summary>
        /// SaveChanges Transaction
        /// for all command
        /// </summary>
        /// <returns></returns>
        public async Task<bool> SaveChangesTransactionAsync()
        {
            bool returnValue = true;
            await using var dbContextTransaction = await Context.Database.BeginTransactionAsync();

            try
            {
                await Context.SaveChangesAsync();
                await dbContextTransaction.CommitAsync();
            }
            catch (Exception)
            {
                //Log Exception Handling message                      
                returnValue = false;
                await dbContextTransaction.RollbackAsync();
            }

            return returnValue;
        }

        /// <summary>
        /// Create new repository for entities
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public ISqlRepository<TEntity> GetRepository<TEntity>()
            where TEntity : class
        {
            // Checks if the Dictionary Key contains the Model class
            if (_repositories.Keys.Contains(typeof(TEntity)))
            {
                // Return the repository for that Model class
                return _repositories[typeof(TEntity)] as ISqlRepository<TEntity>;
            }

            // If the repository for that Model class doesn't exist, create it
            var repository = new SqlRepository<TEntity>(Context);

            // Add it to the dictionary
            _repositories.Add(typeof(TEntity), repository);

            return repository;
        }

        #region IDisposable Support  

        // To detect redundant calls  
        private bool _disposedValue = false; 

        protected virtual void Dispose(bool disposing)
        {
            if (_disposedValue) return;

            if (disposing)
            {
                //dispose managed state (managed objects).  
                Context.Dispose();
            }

            // free unmanaged resources (unmanaged objects) and override a finalizer below.  
            // set large fields to null.  

            _disposedValue = true;
        }

        /// <summary>
        /// override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.  
        /// </summary>
        ~SqlUnitOfWork()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.  
            Dispose(false);
        }

        /// <summary>
        /// This code added to correctly implement the disposable pattern.  
        /// </summary>
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.  
            Dispose(true);

            // uncomment the following line if the finalizer is overridden above.  
            GC.SuppressFinalize(this);  
        }

        #endregion
    }
}