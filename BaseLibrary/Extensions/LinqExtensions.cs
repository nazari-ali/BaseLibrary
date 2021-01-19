using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace BaseLibrary.Extensions
{
    public static class LinqExtensions
    {
        /// <summary>
        /// Orders a list based on a sortexpression. 
        /// Useful in object databinding scenarios where the objectdatasource generates a dynamic sortexpression (example: "Name desc") that specifies the property of the object sort on.
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="sortExpression"></param>
        /// <returns></returns>
        public static IEnumerable<TSource> OrderBy<TSource>(
            this IEnumerable<TSource> source, 
            string sortExpression
        )
        {
            sortExpression += "";
            string[] parts = sortExpression.Split(' ');
            bool descending = false;
            string property = "";

            if (parts.Length > 0 && parts[0] != "")
            {
                property = parts[0];

                if (parts.Length > 1)
                {
                    descending = parts[1].ToLower().Contains("esc");
                }

                PropertyInfo prop = typeof(TSource).GetProperty(property);

                if (prop == null)
                {
                    throw new Exception("No property '" + property + "' in + " + typeof(TSource).Name + "'");
                }

                if (descending)
                {
                    return source.OrderByDescending(x => prop.GetValue(x, null));
                }
                else
                {
                    return source.OrderBy(x => prop.GetValue(x, null));
                }
            }

            return source;
        }

        /// <summary>
        /// When building a LINQ query, you may need to involve optional filtering criteria.
        /// Avoids if statements when building predicates & lambdas for a query.
        /// Useful when you don't know at compile time whether a filter should apply.
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="condition"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static IEnumerable<TSource> WhereIf<TSource>(
            this IEnumerable<TSource> source,
            bool condition, 
            Func<TSource, bool> predicate
        )
        {
            if (condition)
            {
                return source.Where(predicate);
            }
            
            return source;
        }

        /// <summary>
        /// When building a LINQ query, you may need to involve optional filtering criteria.
        /// Avoids if statements when building predicates & lambdas for a query.
        /// Useful when you don't know at compile time whether a filter should apply.
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="condition"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static IEnumerable<TSource> WhereIf<TSource>(
            this IEnumerable<TSource> source,
            bool condition, 
            Func<TSource, int, bool> predicate
        )
        {
            if (condition)
            {
                return source.Where(predicate);
            }

            return source;
        }

        /// <summary>
        /// Converts an enumeration of groupings into a Dictionary of those groupings.
        /// </summary>
        /// <typeparam name="TKey">Key type of the grouping and dictionary.</typeparam>
        /// <typeparam name="TValue">Element type of the grouping and dictionary list.</typeparam>
        /// <param name="groupings">The enumeration of groupings from a GroupBy() clause.</param>
        /// <returns>A dictionary of groupings such that the key of the dictionary is TKey type and the value is List of TValue type.</returns>
        public static Dictionary<TKey, List<TValue>> ToDictionary<TKey, TValue>(
            this IEnumerable<IGrouping<TKey, TValue>> groupings
        )
        {
            return groupings.ToDictionary(group => group.Key, group => group.ToList());
        }

        /// <summary>
        /// I have created this AddRange() method on ObservableCollection because the LINQ Concat() method didn't trigger the CollectionChanged event. This method does.
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="oc"></param>
        /// <param name="collection"></param>
        public static void AddRange<TSource>(
            this ObservableCollection<TSource> oc, 
            IEnumerable<TSource> collection
        )
        {
            if (collection == null)
            {
                throw new ArgumentNullException("collection");
            }

            foreach (var item in collection)
            {
                oc.Add(item);
            }
        }

        /// <summary>
        /// Convert a IEnumerable to a ObservableCollection and can be used in XAML (Silverlight and WPF) projects
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static ObservableCollection<TSource> ToObservableCollection<TSource>(
            this IEnumerable<TSource> source
        )
        {
            var c = new ObservableCollection<TSource>();

            foreach (var e in source)
            {
                c.Add(e);
            }

            return c;
        }

        /// <summary>
        /// rderBy() is nice when you want a consistent & predictable ordering. This method is NOT THAT!
        /// Randomize() - Use this extension method when you want a different or random order every time! 
        /// Useful when ordering a list of things for display to give each a fair chance of landing at the top or bottom on each hit. 
        /// {customers, support techs, or even use as a randomizer for your lottery ;) }
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static IEnumerable<TSource> Randomize<TSource>(
            this IEnumerable<TSource> source
        )
        {
            Random r = new Random();

            return source.OrderBy(x => (r.Next()));
        }

        /// <summary>
        /// if the object this method is called on is not null, runs the given function and returns the value.
        /// if the object is null, returns default(TResult)
        /// </summary>
        public static TResult IfNotNull<TSource, TResult>(
            this TSource source, 
            Func<TSource, TResult> getValue
        )
        {
            if (source != null)
            {
                return getValue(source);
            }

            return default(TResult);
        }

        /// <summary>
        /// Shortcut for foreach and create new list
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static IEnumerable<TSource> ForEach<TSource>(
            this IEnumerable<TSource> source, 
            Action<TSource> action
        )
        {
            foreach (var i in source)
            {
                action(i);
            }

            return source;
        }

        /// <summary>
        /// Shortcut for foreach and create new list
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static IEnumerable<TSource> ForEach<TSource>(
            this IEnumerable source, 
            Action<TSource> action
        )
        {
            return source.Cast<TSource>().ForEach<TSource>(action);
        }

        /// <summary>
        /// Shortcut for foreach and create new list
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="source"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static IEnumerable<TResult> ForEach<TSource, TResult>(
            this IEnumerable<TSource> source, 
            Func<TSource, TResult> predicate
        )
        {
            var list = new List<TResult>();

            foreach (var i in source)
            {
                var obj = predicate(i);

                if (obj != null)
                {
                    list.Add(obj);
                }
            }

            return list;
        }

        /// <summary>
        /// Groups the elements of a sequence according to a specified firstKey selector function and 
        /// rotates the unique values from the secondKey selector function into multiple values in the output, and performs aggregations.
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TFirstKey"></typeparam>
        /// <typeparam name="TSecondKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="source"></param>
        /// <param name="firstKeySelector"></param>
        /// <param name="secondKeySelector"></param>
        /// <param name="aggregate"></param>
        /// <returns></returns>
        public static Dictionary<TFirstKey, Dictionary<TSecondKey, TValue>> Pivot<TSource, TFirstKey, TSecondKey, TValue>(
            this IEnumerable<TSource> source,
            Func<TSource, TFirstKey> firstKeySelector, 
            Func<TSource, TSecondKey> secondKeySelector, 
            Func<IEnumerable<TSource>, TValue> aggregate
        )
        {
            var retVal = new Dictionary<TFirstKey, Dictionary<TSecondKey, TValue>>();
            var l = source.ToLookup(firstKeySelector);

            foreach (var item in l)
            {
                var dict = new Dictionary<TSecondKey, TValue>();

                retVal.Add(item.Key, dict);

                var subdict = item.ToLookup(secondKeySelector);

                foreach (var subitem in subdict)
                {
                    dict.Add(subitem.Key, aggregate(subitem));
                }
            }

            return retVal;
        }

        public static async Task<List<T>> ToListAsync<T>(
            this IEnumerable<T> superset
        )
        {
            return await superset.ToListAsync<T>(CancellationToken.None);
        }

        public static async Task<List<T>> ToListAsync<T>(
            this IEnumerable<T> superset,
            CancellationToken cancellationToken
        )
        {
            return await Task.Run<List<T>>((Func<List<T>>)(() => superset.ToList<T>()), cancellationToken);
        }

        public static string ToSql<TEntity>(
            this IQueryable<TEntity> query
        ) where TEntity : class
        {
            var enumerator = query.Provider.Execute<IEnumerable<TEntity>>(query.Expression).GetEnumerator();
            var relationalCommandCache = enumerator.Private("_relationalCommandCache");
            var selectExpression = relationalCommandCache.Private<SelectExpression>("_selectExpression");
            var factory = relationalCommandCache.Private<IQuerySqlGeneratorFactory>("_querySqlGeneratorFactory");

            var sqlGenerator = factory.Create();
            var command = sqlGenerator.GetCommand(selectExpression);

            string sql = command.CommandText;
            return sql;
        }

        public static IQueryable<TEntity> IncludeMultiple<TEntity>(
            this IQueryable<TEntity> query,
            params Expression<Func<TEntity, object>>[] includes
        )
            where TEntity : class
        {
            if (includes != null)
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }

            return query;
        }

        #region Helper

        private static object Private(this object obj, string privateField) => 
            obj?.GetType().GetField(privateField, BindingFlags.Instance | BindingFlags.NonPublic)?.GetValue(obj);
        private static T Private<T>(this object obj, string privateField) => 
            (T)obj?.GetType().GetField(privateField, BindingFlags.Instance | BindingFlags.NonPublic)?.GetValue(obj);

        #endregion
    }
}