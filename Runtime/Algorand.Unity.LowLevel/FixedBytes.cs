using System;
using System.Runtime.InteropServices;
using Unity.Collections;

namespace Algorand.Unity.LowLevel
{
    [Serializable]
    [StructLayout(LayoutKind.Explicit, Size = 32)]
    public struct FixedBytes32
    {
        [FieldOffset(0)] public FixedBytes30 offset0000;
        [FieldOffset(30)] public byte byte0030;
        [FieldOffset(31)] public byte byte0031;
    }

    [Serializable]
    [StructLayout(LayoutKind.Explicit, Size = 64)]
    public struct FixedBytes64
    {
        [FieldOffset(0)] public FixedBytes62 offset0000;
        [FieldOffset(62)] public byte byte0062;
        [FieldOffset(63)] public byte byte0063;
    }

    [Serializable]
    [StructLayout(LayoutKind.Explicit, Size = 128)]
    public struct FixedBytes128
    {
        [FieldOffset(0)] public FixedBytes126 offset0000;
        [FieldOffset(126)] public byte byte00126;
        [FieldOffset(127)] public byte byte00127;
    }
}
