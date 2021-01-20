using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace BaseLibrary.Mongo.DbContext.UnixSocket
{
    internal static class DomainSocketInfo
    {
        [DllImport("System.Native", EntryPoint = "SystemNative_GetDomainSocketSizes")]
        private static extern void GetDomainSocketSizes(out int pathOffset, out int pathSize, out int addressSize);

        /// <summary>
        /// Path field offset.
        /// </summary>
        public static readonly int PathOffset;

        /// <summary>
        /// Maximum path size (for serialization).
        /// </summary>
        public static readonly int PathSize;

        /// <summary>
        /// Maximum address size (for deserialization).
        /// </summary>
        public static readonly int AddressSize;

        static DomainSocketInfo()
        {
            try
            {
                GetDomainSocketSizes(out PathOffset, out PathSize, out AddressSize);
            }
            catch // fallback to hard-coded values
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    //
                    // typedef unsigned short sa_family_t;
                    //
                    // struct sockaddr_un {
                    //   sa_family_t  sun_family;
                    //   char         sun_path[108];
                    // };
                    //

                    PathOffset = 2;
                    PathSize = 108;
                    AddressSize = 110;
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    //
                    // typedef uint8_t sa_family_t;
                    //
                    // struct sockaddr_un {
                    //   uint8_t      sun_len;
                    //   sa_family_t  sun_family;
                    //   char         sun_path[104];
                    // };
                    //

                    PathOffset = 2;
                    PathSize = 104;
                    AddressSize = 106;
                }
                else
                {
                    // Layout of sockaddr_un structure for OpenBSD and NetBSD is exactly the same as for OS X,
                    // but sun_path length may vary between 92 and 108 bytes, depending on specific OS version
                    // (see http://pubs.opengroup.org/onlinepubs/9699919799/basedefs/sys_un.h.html for details).

                    PathOffset = 2;
                    PathSize = 92;     // worst-case scenario
                    AddressSize = 110; // best-case scenario
                }
            }
        }
    }
}
