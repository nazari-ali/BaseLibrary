using BaseLibrary.MongoDB.Extensions;
using BaseLibrary.MongoDB.UnixSocket.Extensions;
using MongoDB.Driver.Core.Configuration;
using MongoDB.Driver.Core.Connections;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BaseLibrary.MongoDB.UnixSocket
{
    internal sealed class StreamFactory : IStreamFactory
    {
        private readonly IStreamFactory _innerFactory;
        private readonly TcpStreamSettings _settings;

        public StreamFactory(IStreamFactory innerFactory, TcpStreamSettings settings)
        {
            _innerFactory = innerFactory ?? throw new ArgumentNullException(nameof(innerFactory));
            _settings = settings ?? new TcpStreamSettings();
        }

        #region IStreamFactory implementation

        public Stream CreateStream(EndPoint endPoint, CancellationToken cancellationToken)
        {
            return IsUnixEndPoint(endPoint, out var unixEndPoint)
                ? CreateStream(unixEndPoint, cancellationToken)
                : _innerFactory.CreateStream(endPoint, cancellationToken);
        }

        public Task<Stream> CreateStreamAsync(EndPoint endPoint, CancellationToken cancellationToken)
        {
            return IsUnixEndPoint(endPoint, out var unixEndPoint)
                ? CreateStreamAsync(unixEndPoint, cancellationToken)
                : _innerFactory.CreateStreamAsync(endPoint, cancellationToken);
        }

        #endregion

        private static bool IsUnixEndPoint(EndPoint endPoint, out UnixEndPoint unixEndPoint)
        {
            if (endPoint is DnsEndPoint dnsEndPoint)
            {
                var path = WebUtility.UrlDecode(dnsEndPoint.Host);
                if (Path.GetExtension(path) == ".sock")
                {
                    unixEndPoint = new UnixEndPoint(path);
                    return true;
                }
            }

            unixEndPoint = null;
            return false;
        }

        private static Socket CreateUnixDomainSocket() => new Socket(AddressFamily.Unix, SocketType.Stream, ProtocolType.IP);

        private Stream CreateSocketStream(Socket socket)
        {
            socket.ReceiveBufferSize = _settings.ReceiveBufferSize;
            socket.SendBufferSize = _settings.SendBufferSize;

            var stream = new NetworkStream(socket, true);

            if (_settings.ReadTimeout.HasValue && _settings.ReadTimeout.Value > TimeSpan.Zero)
                stream.ReadTimeout = (int)_settings.ReadTimeout.Value.TotalMilliseconds;

            if (_settings.WriteTimeout.HasValue && _settings.WriteTimeout.Value > TimeSpan.Zero)
                stream.WriteTimeout = (int)_settings.WriteTimeout.Value.TotalMilliseconds;

            return stream;
        }

        private static void CancelSocketConnect(object state)
        {
            var socket = (Socket)state;
            if (!socket.Connected)
                socket.SafeDispose();
        }

        private void Connect(Socket socket, EndPoint endPoint, CancellationToken cancellationToken)
        {
            using (var timeoutTokenSource = new CancellationTokenSource(_settings.ConnectTimeout))
            using (var linkedTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, timeoutTokenSource.Token))
            using (linkedTokenSource.Token.Register(CancelSocketConnect, socket))
            {
                try
                {
                    socket.Connect(endPoint);
                }
                catch when (linkedTokenSource.IsCancellationRequested)
                {
                    socket.SafeDispose();
                    cancellationToken.ThrowIfCancellationRequested();
                    throw new TimeoutException($"Timed out connecting to {endPoint}. Timeout was {_settings.ConnectTimeout}.");
                }
                catch
                {
                    socket.SafeDispose();
                    throw;
                }
            }
        }

        private Stream CreateStream(UnixEndPoint endPoint, CancellationToken cancellationToken)
        {
            var socket = CreateUnixDomainSocket();
            Connect(socket, endPoint, cancellationToken);
            return CreateSocketStream(socket);
        }

        private async Task ConnectAsync(Socket socket, EndPoint endPoint, CancellationToken cancellationToken)
        {
            using (var timeoutTokenSource = new CancellationTokenSource(_settings.ConnectTimeout))
            using (var linkedTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, timeoutTokenSource.Token))
            using (linkedTokenSource.Token.Register(CancelSocketConnect, socket))
            {
                try
                {
                    #if NETSTANDARD 
                    await socket.ConnectAsync(endPoint).ConfigureAwait(false);
                    #else
                    await Task.Factory.FromAsync(socket.BeginConnect(endPoint, null, null), socket.EndConnect).ConfigureAwait(false);
                    #endif
                }
                catch when (linkedTokenSource.IsCancellationRequested)
                {
                    socket.SafeDispose();
                    cancellationToken.ThrowIfCancellationRequested();
                    throw new TimeoutException($"Timed out connecting to {endPoint}. Timeout was {_settings.ConnectTimeout}.");
                }
                catch
                {
                    socket.SafeDispose();
                    throw;
                }
            }
        }

        private async Task<Stream> CreateStreamAsync(UnixEndPoint endPoint, CancellationToken cancellationToken)
        {
            var socket = CreateUnixDomainSocket();
            await ConnectAsync(socket, endPoint, cancellationToken);
            return CreateSocketStream(socket);
        }
    }
}
