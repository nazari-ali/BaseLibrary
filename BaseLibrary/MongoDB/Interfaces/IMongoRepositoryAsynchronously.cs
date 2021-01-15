using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace BaseLibrary.MongoDB.Interfaces
{
    public interface IMongoRepositoryAsynchronously<TDocument> 
        where TDocument : class
    {
        Task<TDocument> FindOneAsync(
            Expression<Func<TDocument, bool>> filterExpression,
            CancellationToken cancellationToken = default
        );

        Task<TDocument> FindByIdAsync(
            string objectId,
            CancellationToken cancellationToken = default
        );

        Task InsertOneAsync(
            TDocument document,
            CancellationToken cancellationToken = default
        );

        Task InsertManyAsync(
            ICollection<TDocument> documents,
            CancellationToken cancellationToken = default
        );

        Task ReplaceOneAsync(
            TDocument document,
            CancellationToken cancellationToken = default
        );

        Task DeleteOneAsync(
            Expression<Func<TDocument, bool>> filterExpression,
            CancellationToken cancellationToken = default
        );

        Task DeleteByIdAsync(
            string objectId,
            CancellationToken cancellationToken = default
        );

        Task DeleteManyAsync(
            Expression<Func<TDocument, bool>> filterExpression,
            CancellationToken cancellationToken = default
        );
    }
}