using System;
using AlgoSdk.Formatters;
using AlgoSdk.LowLevel;

namespace AlgoSdk
{
    [AlgoApiFormatter(typeof(ByteArrayFormatter<VrfPubkey>))]
    public struct VrfPubkey
        : IByteArray
        , IEquatable<VrfPubkey>
    {
        public byte this[int index] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public unsafe void* GetUnsafePtr() => (void*)IntPtr.Zero;

        public int Length => 0;

        public bool Equals(VrfPubkey other)
        {
            return ByteArray.Equals(in this, in other);
        }

        public override bool Equals(object obj)
        {
            return ByteArray.Equals(in this, obj);
        }

        public override int GetHashCode()
        {
            return ByteArray.GetHashCode(in this);
        }

        public static bool operator ==(in VrfPubkey x, in VrfPubkey y)
        {
            return ByteArray.Equals(in x, in y);
        }

        public static bool operator !=(in VrfPubkey x, in VrfPubkey y)
        {
            return !ByteArray.Equals(in x, in y);
        }
    }
}
