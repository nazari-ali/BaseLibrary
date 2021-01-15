using BaseLibrary.MongoDB.Interfaces;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;

namespace BaseLibrary.MongoDB
{
    public class MongoUnitOfWork : IMongoUnitOfWork
    {
        protected readonly IMongoContext MongoContext;

        public MongoUnitOfWork(IMongoContext mongoContext)
        {
            MongoContext = mongoContext;
        }

        /// <summary>
        /// SaveChanges
        /// </summary>
        /// <returns></returns>
        public void SaveChanges()
        {
            MongoContext.SaveChanges();
        }

        /// <summary>
        /// SaveChanges Transaction
        /// for all command
        /// </summary>
        /// <returns></returns>
        public bool SaveChangesTransaction()
        {

            return MongoContext.SaveChangesTransaction();
        }

        /// <summary>
        /// SaveChanges Transaction
        /// for all command
        /// </summary>
        /// <returns></returns>
        public Task<bool> SaveChangesTransactionAsync()
        {
            return MongoContext.SaveChangesTransactionAsync();
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
                MongoContext.Dispose();
            }

            // free unmanaged resources (unmanaged objects) and override a finalizer below.  
            // set large fields to null.  

            _disposedValue = true;
        }

        /// <summary>
        /// override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.  
        /// </summary>
        ~MongoUnitOfWork()
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