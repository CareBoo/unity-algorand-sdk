using System;
using System.Runtime.InteropServices;

namespace Algorand.Unity.Crypto
{
    public partial struct PwHash
    {
        [Serializable]
        [StructLayout(LayoutKind.Explicit, Size = SizeBytes)]
        public struct Salt
        {
            public const int SizeBytes = sodium.crypto_pwhash_SALTBYTES;
            [FieldOffset(0), NonSerialized] public unsafe fixed byte bytes[SizeBytes];
            [FieldOffset(0)] public unsafe fixed ulong ulongs[SizeBytes / sizeof(ulong)];
        }
    }
}
