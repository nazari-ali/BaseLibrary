using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace BaseLibrary.Mongo.Repository.Interfaces
{
    public interface IMongoRepositorySynchronously<TDocument> 
        where TDocument : class
    {
        IQueryable<TDocument> AsQueryable();

        IEnumerable<TDocument> FilterBy(
            Expression<Func<TDocument, bool>> filterExpression
        );

        IEnumerable<TProjected> FilterBy<TProjected>(
            Expression<Func<TDocument, bool>> filterExpression,
            Expression<Func<TDocument, TProjected>> projectionExpression
        );

        TDocument FindOne(
            Expression<Func<TDocument, bool>> filterExpression
        );

        TDocument FindById(
            string objectId
        );

        void InsertOne(
            TDocument document
        );

        void InsertMany(
            ICollection<TDocument> documents
        );

        void ReplaceOne(
            TDocument document
        );

        void DeleteOne(
            Expression<Func<TDocument, bool>> filterExpression
        );

        void DeleteById(
            string objectId
        );

        void DeleteMany(
            Expression<Func<TDocument, bool>> filterExpression
        );
    }
}