using BaseLibrary.MongoDB.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace BaseLibrary.MongoDB
{
    public class MongoRepository<TDocument> : IMongoRepository<TDocument>, IGridFsRepository
        where TDocument : IDocument
    {
        protected readonly IGridFSBucket Bucket;
        protected readonly IMongoCollection<TDocument> Collection;
        private readonly IClientSessionHandle _session;
        private readonly IMongoContext _mongoContext;

        public MongoRepository(IMongoContext mongoContext)
        {
            Collection = mongoContext.GetCollection<TDocument>();

            var bucketSettings = mongoContext.GridFsSettings;

            Bucket = new GridFSBucket(mongoContext.Database, new GridFSBucketOptions
            {
                BucketName = bucketSettings.BucketName,
                ChunkSizeBytes = bucketSettings.BucketSize,
                WriteConcern = WriteConcern.WMajority,
                ReadPreference = ReadPreference.Primary
            });

            _mongoContext = mongoContext;
            _session = mongoContext.Session;
        }

        #region IRepositorySynchronously

        public virtual IQueryable<TDocument> AsQueryable()
        {
            return Collection.AsQueryable();
        }

        public virtual IEnumerable<TDocument> FilterBy(
            Expression<Func<TDocument, bool>> filterExpression
        )
        {
            return Collection.Find(filterExpression).ToEnumerable();
        }

        public virtual IEnumerable<TProjected> FilterBy<TProjected>(
            Expression<Func<TDocument, bool>> filterExpression,
            Expression<Func<TDocument, TProjected>> projectionExpression
        )
        {
            return Collection.Find(filterExpression).Project(projectionExpression).ToEnumerable();
        }

        public virtual TDocument FindOne(
            Expression<Func<TDocument, bool>> filterExpression
        )
        {
            return Collection.Find(filterExpression).FirstOrDefault();
        }

        public virtual TDocument FindById(
            string id
        )
        {
            var objectId = new ObjectId(id);
            var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, objectId);
            return Collection.Find(filter).SingleOrDefault();
        }

        public virtual void InsertOne(
            TDocument document
        )
        {
            _mongoContext.AddCommand(() =>
                Collection.InsertOne(_session, document)
            );
        }

        public void InsertMany(
            ICollection<TDocument> documents
        )
        {
            _mongoContext.AddCommand(() =>
                Collection.InsertMany(_session, documents)
            );
        }

        public void ReplaceOne(
            TDocument document
        )
        {
            var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, document.Id);

            _mongoContext.AddCommand(() =>
                Collection.FindOneAndReplace(_session, filter, document)
            );
        }

        public void DeleteOne(
            Expression<Func<TDocument, bool>> filterExpression
        )
        {
            _mongoContext.AddCommand(() =>
                Collection.FindOneAndDelete(_session, filterExpression)
            );
        }

        public void DeleteById(
            string id
        )
        {
            var objectId = new ObjectId(id);
            var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, objectId);

            _mongoContext.AddCommand(() =>
                Collection.FindOneAndDelete(_session, filter)
            );
        }

        public void DeleteMany(
            Expression<Func<TDocument, bool>> filterExpression
        )
        {
            _mongoContext.AddCommand(() =>
                Collection.DeleteMany(_session, filterExpression)
            );;
        }

        #endregion

        #region IRepositoryAsynchronously

        public virtual Task<TDocument> FindOneAsync(
            Expression<Func<TDocument, bool>> filterExpression,
            CancellationToken cancellationToken = default
        )
        {
            return Task.Run(() => Collection.Find(filterExpression).FirstOrDefaultAsync(cancellationToken));
        }

        public virtual Task<TDocument> FindByIdAsync(
            string id,
            CancellationToken cancellationToken = default
        )
        {
            return Task.Run(() =>
            {
                var objectId = new ObjectId(id);
                var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, objectId);

                return Collection.Find(filter).SingleOrDefaultAsync(cancellationToken);
            });
        }

        public virtual Task InsertOneAsync(
            TDocument document,
            CancellationToken cancellationToken = default
        )
        {
            return _mongoContext.AddCommand(() =>
                Collection.InsertOneAsync(_session, document, null, cancellationToken)
            );
        }

        public virtual Task InsertManyAsync(
            ICollection<TDocument> documents,
            CancellationToken cancellationToken = default
        )
        {
            return _mongoContext.AddCommand(() =>
                Collection.InsertManyAsync(_session, documents, null, cancellationToken)
            );
        }

        public virtual Task ReplaceOneAsync(
            TDocument document,
            CancellationToken cancellationToken = default
        )
        {
            var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, document.Id);

            return _mongoContext.AddCommand(() =>
                Collection.FindOneAndReplaceAsync(_session, filter, document, null, cancellationToken)
            );
        }

        public Task DeleteOneAsync(
            Expression<Func<TDocument, bool>> filterExpression,
            CancellationToken cancellationToken = default
        )
        {
            return _mongoContext.AddCommand(() =>
                Collection.FindOneAndDeleteAsync(_session, filterExpression, null, cancellationToken)
            );
        }

        public Task DeleteByIdAsync(
            string id,
            CancellationToken cancellationToken = default
        )
        {
            var objectId = new ObjectId(id);
            var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, objectId);

            return _mongoContext.AddCommand(() =>
                Collection.FindOneAndDeleteAsync(_session, filter, null, cancellationToken)
            );
        }

        public Task DeleteManyAsync(
            Expression<Func<TDocument, bool>> filterExpression,
            CancellationToken cancellationToken = default
        )
        {
            return _mongoContext.AddCommand(() =>
                Collection.DeleteManyAsync(_session, filterExpression, null, cancellationToken)
            );
        }

        #endregion

        #region IGridFsRepository

        public GridFSFileInfo GetGridFsInfoById(ObjectId id)
        {
            var filter = Builders<GridFSFileInfo>.Filter.Eq("_id", id);
            return Bucket.Find(filter).FirstOrDefault();
        }

        public async Task<GridFSFileInfo> GetGridFsInfoByIdAsync(ObjectId id)
        {
            var filter = Builders<GridFSFileInfo>.Filter.Eq("_id", id);
            return await Bucket.FindAsync(filter).Result.FirstOrDefaultAsync();
        }

        public GridFSFileInfo GetGridFsInfoByFileName(string fileName)
        {
            var filter = Builders<GridFSFileInfo>.Filter.Eq("filename", fileName);
            return Bucket.Find(filter).FirstOrDefault();
        }

        public async Task<GridFSFileInfo> GetGridFsInfoByFileNameAsync(string fileName)
        {
            var filter = Builders<GridFSFileInfo>.Filter.Eq("filename", fileName);
            return await Bucket.FindAsync(filter).Result.FirstOrDefaultAsync();
        }

        #endregion
    }
}