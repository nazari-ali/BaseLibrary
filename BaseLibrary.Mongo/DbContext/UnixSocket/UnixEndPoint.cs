using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace BaseLibrary.Mongo.DbContext.UnixSocket
{
    internal sealed class UnixEndPoint : EndPoint
    {
        private static readonly byte[] EmptyPathBytes = new byte[0];
        private static readonly Encoding PathEncoding = Encoding.UTF8;
        private readonly string _path;
        private readonly byte[] _pathBytes;

        public UnixEndPoint(string path)
        {
            if (path == null)
                throw new ArgumentNullException(nameof(path));
            if (path.Length == 0)
                throw new ArgumentException("Path cannot be empty", nameof(path));

            var pathSize = PathEncoding.GetByteCount(path) + 1;
            if (pathSize > DomainSocketInfo.PathSize)
                throw new ArgumentException("Path is too long", nameof(path));

            _path = path;
            _pathBytes = new byte[pathSize];
            PathEncoding.GetBytes(path, 0, path.Length, _pathBytes, 0);
        }

        private UnixEndPoint(SocketAddress socketAddress)
        {
            if (socketAddress == null)
                throw new ArgumentNullException(nameof(socketAddress));
            if (socketAddress.Family != AddressFamily.Unix)
                throw new ArgumentException("Invalid address family", nameof(socketAddress));
            if (socketAddress.Size > DomainSocketInfo.AddressSize)
                throw new ArgumentException("Invalid address length", nameof(socketAddress));

            _path = string.Empty;
            _pathBytes = EmptyPathBytes;

            var pathSize = socketAddress.Size - DomainSocketInfo.PathOffset;
            if (pathSize > 0)
            {
                _pathBytes = new byte[pathSize];
                for (var index = 0; index < pathSize; index++)
                {
                    _pathBytes[index] = socketAddress[DomainSocketInfo.PathOffset + index];
                    if (_pathBytes[index] == 0)
                    {
                        // stop at zero terminator
                        pathSize = index;
                        break;
                    }
                }

                if (pathSize > 0)
                {
                    // socket path is not empty, decode it from address bytes
                    _path = PathEncoding.GetString(_pathBytes, 0, pathSize);
                }
            }
        }

        /// <inheritdoc />
        public override EndPoint Create(SocketAddress socketAddress) => new UnixEndPoint(socketAddress);

        /// <inheritdoc />
        public override SocketAddress Serialize()
        {
            var addressForSerialize = new SocketAddress(AddressFamily, DomainSocketInfo.PathOffset + _pathBytes.Length);
            for (var index = 0; index < _pathBytes.Length; index++)
                addressForSerialize[DomainSocketInfo.PathOffset + index] = _pathBytes[index];
            return addressForSerialize;
        }

        /// <inheritdoc />
        public override AddressFamily AddressFamily => AddressFamily.Unix;

        /// <inheritdoc />
        public override string ToString() => _path;

        /// <inheritdoc />
        public override int GetHashCode() => _path.GetHashCode();

        /// <inheritdoc />
        public override bool Equals(object obj) => obj is UnixEndPoint other && _path == other._path;
    }
}
