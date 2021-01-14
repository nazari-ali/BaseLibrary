using System;
using System.Collections.Generic;
using System.Text;

namespace BaseLibrary.MongoDB.UnixSocket.Extensions
{
    internal static class DisposableExtensions
    {
        public static void SafeDispose(this IDisposable disposable)
        {
            try
            {
                disposable?.Dispose();
            }
            catch
            {
                // ignored
            }
        }
    }
}
