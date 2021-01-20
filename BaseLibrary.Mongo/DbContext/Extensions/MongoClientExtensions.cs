using BaseLibrary.Mongo.DbContext.UnixSocket;
using BaseLibrary.Mongo.DbContext.UnixSocket.Extensions;
using MongoDB.Driver;
using MongoDB.Driver.Core.Configuration;
using System;

namespace BaseLibrary.Mongo.DbContext.Extensions
{
    /// <summary>
    /// Extension methods for Unix domain sockets configuration.
    /// </summary>
    public static class MongoClientExtensions
    {
        /// <summary>
        /// Creates a frozen copy of <paramref name="settings"/> with Unix domain sockets enabled.
        /// </summary>
        /// <param name="settings">MongoDB client settings.</param>
        public static MongoClientSettings WithUnixDomainSockets(this MongoClientSettings settings)
        {
            if (settings == null)
                throw new ArgumentNullException(nameof(settings));

            var existingConfigurator = settings.ClusterConfigurator;

            settings.ClusterConfigurator = clusterBuilder =>
            {
                clusterBuilder.EnableUnixDomainSockets();
                existingConfigurator?.Invoke(clusterBuilder);
            };

            return settings.FrozenCopy();
        }

        /// <summary>
        /// Enables Unix domain sockets support for <paramref name="builder"/>.
        /// </summary>
        /// <param name="builder">Cluster builder.</param>
        public static ClusterBuilder EnableUnixDomainSockets(this ClusterBuilder builder)
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder));

            return builder.DoRegisterStreamFactory(inner => new StreamFactory(inner, builder.GetTcpStreamSettings()));
        }
    }
}
