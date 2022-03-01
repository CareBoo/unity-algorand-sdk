using System;
using System.Runtime.InteropServices;

namespace AlgoSdk.Crypto
{
    internal static unsafe partial class sodium
    {
        [DllImport(Library, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void randombytes_buf(void* buf, UIntPtr size);
    }
}
