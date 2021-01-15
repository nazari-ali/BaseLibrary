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
            bool returnValue = true;
            MongoContext.Session.StartTransaction();

            try
            {
                SaveChanges();
                MongoContext.Session.CommitTransaction();
            }
            catch (Exception)
            {
                returnValue = false;
                MongoContext.Session.AbortTransaction();
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
            MongoContext.Session.StartTransaction();

            try
            {
                SaveChanges();
                await MongoContext.Session.CommitTransactionAsync();
            }
            catch (Exception)
            {
                returnValue = false;
                await MongoContext.Session.AbortTransactionAsync();
            }

            return returnValue;
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