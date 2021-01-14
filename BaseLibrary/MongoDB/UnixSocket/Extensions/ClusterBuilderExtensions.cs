using MongoDB.Driver.Core.Configuration;
using MongoDB.Driver.Core.Connections;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text;

namespace BaseLibrary.MongoDB.UnixSocket.Extensions
{
    internal static class ClusterBuilderExtensions
    {
        private const string RegisterStreamFactoryFixedInVersion = "2.7.3.0";

        private static readonly FieldInfo _tcpStreamSettings;
        private static readonly FieldInfo _streamFactoryWrapper;
        private static readonly bool CanRegisterStreamFactory = false;

        static ClusterBuilderExtensions()
        {
            var clusterBuilderTypeInfo = typeof(ClusterBuilder).GetTypeInfo();
            const BindingFlags flags = BindingFlags.NonPublic | BindingFlags.Instance;

            _tcpStreamSettings = clusterBuilderTypeInfo.GetField(nameof(_tcpStreamSettings), flags);
            _streamFactoryWrapper = clusterBuilderTypeInfo.GetField(nameof(_streamFactoryWrapper), flags);

            var driverVersion = clusterBuilderTypeInfo.Assembly.GetName().Version;
            CanRegisterStreamFactory = driverVersion >= Version.Parse(RegisterStreamFactoryFixedInVersion);
        }

        public static TcpStreamSettings GetTcpStreamSettings(this ClusterBuilder clusterBuilder)
        {
            if (clusterBuilder == null)
                throw new ArgumentNullException(nameof(clusterBuilder));

            Debug.Assert(_tcpStreamSettings != null);

            return (TcpStreamSettings)_tcpStreamSettings.GetValue(clusterBuilder);
        }

        public static ClusterBuilder DoRegisterStreamFactory(this ClusterBuilder clusterBuilder, Func<IStreamFactory, IStreamFactory> wrapper)
        {
            if (clusterBuilder == null)
                throw new ArgumentNullException(nameof(clusterBuilder));
            if (wrapper == null)
                throw new ArgumentNullException(nameof(wrapper));

            if (CanRegisterStreamFactory)
            {
                // native RegisterStreamFactory is known to be working
                return clusterBuilder.RegisterStreamFactory(wrapper);
            }

            Debug.Assert(_streamFactoryWrapper != null);

            var streamFactoryWrapper = (Func<IStreamFactory, IStreamFactory>)_streamFactoryWrapper.GetValue(clusterBuilder);
            _streamFactoryWrapper.SetValue(clusterBuilder, (Func<IStreamFactory, IStreamFactory>)(inner => wrapper(streamFactoryWrapper(inner))));
            return clusterBuilder;
        }
    }
}
